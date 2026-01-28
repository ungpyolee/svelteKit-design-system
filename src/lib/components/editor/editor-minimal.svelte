<!-- $lib/components/editor/editor-minimal.svelte -->
<!-- Notion 스타일의 미니멀 에디터 -->
<script>
    import { onMount, onDestroy } from 'svelte';
    import Icon from "$lib/icons/icon.svelte";

    let {
        value = '',
        placeholder = '내용을 입력하세요...',
        onChange = () => {},
        minHeight = '300px'
    } = $props();

    let editorElement;
    let toolbarVisible = $state(false);
    let toolbarPosition = $state({ top: 0, left: 0 });
    let showLinkInput = $state(false);
    let linkUrl = $state('');
    let savedSelection = $state(null);

    // 툴바 상태
    let activeFormats = $state({
        bold: false,
        italic: false,
        underline: false,
        strikethrough: false,
        h1: false,
        h2: false,
        h3: false,
        quote: false,
        code: false,
        link: false
    });

    onMount(() => {
        if (value) {
            editorElement.innerHTML = value;
        }

        document.addEventListener('selectionchange', handleSelectionChange);
        document.addEventListener('mousedown', handleClickOutside);
    });

    onDestroy(() => {
        document.removeEventListener('selectionchange', handleSelectionChange);
        document.removeEventListener('mousedown', handleClickOutside);
    });

    function handleClickOutside(e) {
        if (!e.target.closest('.minimal-toolbar') && !e.target.closest('.editor-content')) {
            toolbarVisible = false;
            showLinkInput = false;
        }
    }

    function handleSelectionChange() {
        const selection = window.getSelection();
        if (!selection.rangeCount) return;

        const range = selection.getRangeAt(0);

        // 에디터 내부 선택인지 확인
        if (!editorElement?.contains(range.commonAncestorContainer)) {
            return;
        }

        // 선택 영역이 있을 때만 툴바 표시
        if (!selection.isCollapsed && selection.toString().trim()) {
            const rect = range.getBoundingClientRect();
            const editorRect = editorElement.getBoundingClientRect();

            toolbarPosition = {
                top: rect.top - editorRect.top - 48,
                left: Math.max(0, rect.left - editorRect.left + (rect.width / 2) - 150)
            };

            updateActiveFormats();
            toolbarVisible = true;
        } else {
            toolbarVisible = false;
            showLinkInput = false;
        }
    }

    function updateActiveFormats() {
        activeFormats = {
            bold: document.queryCommandState('bold'),
            italic: document.queryCommandState('italic'),
            underline: document.queryCommandState('underline'),
            strikethrough: document.queryCommandState('strikeThrough'),
            h1: isHeadingActive('H1'),
            h2: isHeadingActive('H2'),
            h3: isHeadingActive('H3'),
            quote: isBlockActive('BLOCKQUOTE'),
            code: document.queryCommandState('insertHTML') || isBlockActive('CODE'),
            link: isLinkActive()
        };
    }

    function isHeadingActive(tag) {
        const selection = window.getSelection();
        if (!selection.rangeCount) return false;
        let node = selection.anchorNode;
        while (node) {
            if (node.nodeName === tag) return true;
            node = node.parentNode;
        }
        return false;
    }

    function isBlockActive(tag) {
        const selection = window.getSelection();
        if (!selection.rangeCount) return false;
        let node = selection.anchorNode;
        while (node) {
            if (node.nodeName === tag) return true;
            node = node.parentNode;
        }
        return false;
    }

    function isLinkActive() {
        const selection = window.getSelection();
        if (!selection.rangeCount) return false;
        let node = selection.anchorNode;
        while (node) {
            if (node.nodeName === 'A') return true;
            node = node.parentNode;
        }
        return false;
    }

    function execCommand(command, value = null) {
        editorElement.focus();
        document.execCommand(command, false, value);
        updateActiveFormats();
        handleInput();
    }

    function formatBlock(tag) {
        editorElement.focus();
        document.execCommand('formatBlock', false, tag);
        updateActiveFormats();
        handleInput();
    }

    function toggleLink() {
        if (isLinkActive()) {
            document.execCommand('unlink', false, null);
            updateActiveFormats();
            handleInput();
        } else {
            // 현재 선택 저장
            const selection = window.getSelection();
            if (selection.rangeCount > 0) {
                savedSelection = selection.getRangeAt(0).cloneRange();
            }
            showLinkInput = true;
            linkUrl = '';
        }
    }

    function insertLink() {
        if (!linkUrl.trim()) {
            showLinkInput = false;
            return;
        }

        // URL 형식 보정
        let url = linkUrl.trim();
        if (!url.startsWith('http://') && !url.startsWith('https://')) {
            url = 'https://' + url;
        }

        // 저장된 선택 복원
        if (savedSelection) {
            const selection = window.getSelection();
            selection.removeAllRanges();
            selection.addRange(savedSelection);
        }

        editorElement.focus();
        document.execCommand('createLink', false, url);

        showLinkInput = false;
        linkUrl = '';
        savedSelection = null;
        updateActiveFormats();
        handleInput();
    }

    function handleLinkKeydown(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            insertLink();
        } else if (e.key === 'Escape') {
            showLinkInput = false;
            linkUrl = '';
        }
    }

    function handleInput() {
        onChange(editorElement.innerHTML);
    }

    function handleKeydown(e) {
        // Tab 들여쓰기
        if (e.key === 'Tab') {
            e.preventDefault();
            document.execCommand('insertHTML', false, '&nbsp;&nbsp;&nbsp;&nbsp;');
        }

        // 키보드 단축키
        if (e.ctrlKey || e.metaKey) {
            switch (e.key.toLowerCase()) {
                case 'b':
                    e.preventDefault();
                    execCommand('bold');
                    break;
                case 'i':
                    e.preventDefault();
                    execCommand('italic');
                    break;
                case 'u':
                    e.preventDefault();
                    execCommand('underline');
                    break;
                case 'k':
                    e.preventDefault();
                    toggleLink();
                    break;
            }
        }
    }

    function handlePaste(e) {
        e.preventDefault();
        const text = e.clipboardData.getData('text/plain');
        document.execCommand('insertText', false, text);
    }

    // 외부 API
    export function getHTML() {
        return editorElement?.innerHTML || '';
    }

    export function getText() {
        return editorElement?.innerText || '';
    }

    export function setHTML(html) {
        if (editorElement) {
            editorElement.innerHTML = html;
            handleInput();
        }
    }

    export function clear() {
        if (editorElement) {
            editorElement.innerHTML = '';
            handleInput();
        }
    }

    export function focus() {
        editorElement?.focus();
    }
</script>

<div class="minimal-editor relative">
    <!-- 플로팅 툴바 -->
    {#if toolbarVisible}
        <div
            class="minimal-toolbar absolute z-50 flex items-center gap-0.5 px-1.5 py-1 bg-gray-900 dark:bg-gray-800 rounded-lg shadow-xl border border-gray-700 dark:border-gray-600"
            style="top: {toolbarPosition.top}px; left: {toolbarPosition.left}px;"
        >
            {#if showLinkInput}
                <!-- 링크 입력 -->
                <div class="flex items-center gap-2 px-2">
                    <input
                        type="text"
                        bind:value={linkUrl}
                        onkeydown={handleLinkKeydown}
                        placeholder="URL 입력"
                        class="w-48 px-2 py-1 text-sm bg-gray-800 dark:bg-gray-700 text-white border border-gray-600 rounded focus:outline-none focus:border-primary"
                        autofocus
                    />
                    <button
                        type="button"
                        onclick={insertLink}
                        class="p-1 text-white hover:text-primary transition-colors"
                    >
                        <Icon name="Check" size="sm" />
                    </button>
                    <button
                        type="button"
                        onclick={() => { showLinkInput = false; linkUrl = ''; }}
                        class="p-1 text-gray-400 hover:text-white transition-colors"
                    >
                        <Icon name="X" size="sm" />
                    </button>
                </div>
            {:else}
                <!-- 서식 버튼 -->
                <div class="flex items-center">
                    <!-- 텍스트 스타일 -->
                    <button
                        type="button"
                        onclick={() => execCommand('bold')}
                        class="toolbar-btn {activeFormats.bold ? 'active' : ''}"
                        title="굵게 (Ctrl+B)"
                    >
                        <Icon name="Bold" size="sm" />
                    </button>
                    <button
                        type="button"
                        onclick={() => execCommand('italic')}
                        class="toolbar-btn {activeFormats.italic ? 'active' : ''}"
                        title="기울임 (Ctrl+I)"
                    >
                        <Icon name="Italic" size="sm" />
                    </button>
                    <button
                        type="button"
                        onclick={() => execCommand('underline')}
                        class="toolbar-btn {activeFormats.underline ? 'active' : ''}"
                        title="밑줄 (Ctrl+U)"
                    >
                        <Icon name="Underline" size="sm" />
                    </button>
                    <button
                        type="button"
                        onclick={() => execCommand('strikeThrough')}
                        class="toolbar-btn {activeFormats.strikethrough ? 'active' : ''}"
                        title="취소선"
                    >
                        <Icon name="Strikethrough" size="sm" />
                    </button>

                    <div class="w-px h-5 bg-gray-600 mx-1"></div>

                    <!-- 헤딩 -->
                    <button
                        type="button"
                        onclick={() => formatBlock('h2')}
                        class="toolbar-btn {activeFormats.h2 ? 'active' : ''}"
                        title="제목 1"
                    >
                        <span class="text-xs font-bold">H1</span>
                    </button>
                    <button
                        type="button"
                        onclick={() => formatBlock('h3')}
                        class="toolbar-btn {activeFormats.h3 ? 'active' : ''}"
                        title="제목 2"
                    >
                        <span class="text-xs font-bold">H2</span>
                    </button>

                    <div class="w-px h-5 bg-gray-600 mx-1"></div>

                    <!-- 블록 스타일 -->
                    <button
                        type="button"
                        onclick={() => formatBlock('blockquote')}
                        class="toolbar-btn {activeFormats.quote ? 'active' : ''}"
                        title="인용"
                    >
                        <Icon name="Quote" size="sm" />
                    </button>
                    <button
                        type="button"
                        onclick={() => execCommand('insertUnorderedList')}
                        class="toolbar-btn"
                        title="목록"
                    >
                        <Icon name="List" size="sm" />
                    </button>
                    <button
                        type="button"
                        onclick={() => execCommand('insertOrderedList')}
                        class="toolbar-btn"
                        title="번호 목록"
                    >
                        <Icon name="ListOrdered" size="sm" />
                    </button>

                    <div class="w-px h-5 bg-gray-600 mx-1"></div>

                    <!-- 링크 -->
                    <button
                        type="button"
                        onclick={toggleLink}
                        class="toolbar-btn {activeFormats.link ? 'active' : ''}"
                        title="링크 (Ctrl+K)"
                    >
                        <Icon name="Link" size="sm" />
                    </button>
                </div>
            {/if}
        </div>
    {/if}

    <!-- 에디터 본문 -->
    <div
        bind:this={editorElement}
        contenteditable="true"
        class="editor-content outline-none text-gray-800 dark:text-gray-200 leading-relaxed"
        style="min-height: {minHeight};"
        oninput={handleInput}
        onkeydown={handleKeydown}
        onpaste={handlePaste}
        data-placeholder={placeholder}
    ></div>
</div>

<style>
    .editor-content {
        @apply px-0 py-2;
    }

    .editor-content:empty:before {
        content: attr(data-placeholder);
        @apply text-gray-400 dark:text-gray-500 pointer-events-none;
    }

    .editor-content:focus:empty:before {
        content: attr(data-placeholder);
    }

    /* 기본 텍스트 스타일 */
    :global(.editor-content p) {
        @apply mb-3;
    }

    :global(.editor-content h1),
    :global(.editor-content h2) {
        @apply text-2xl font-bold mb-4 mt-6 text-gray-900 dark:text-gray-100;
    }

    :global(.editor-content h3) {
        @apply text-xl font-semibold mb-3 mt-5 text-gray-900 dark:text-gray-100;
    }

    :global(.editor-content h4) {
        @apply text-lg font-semibold mb-2 mt-4 text-gray-900 dark:text-gray-100;
    }

    :global(.editor-content blockquote) {
        @apply border-l-4 border-primary pl-4 py-1 my-4 text-gray-600 dark:text-gray-400 italic bg-gray-50 dark:bg-gray-800/50 rounded-r-lg pr-4;
    }

    :global(.editor-content ul) {
        @apply list-disc list-inside mb-3 space-y-1;
    }

    :global(.editor-content ol) {
        @apply list-decimal list-inside mb-3 space-y-1;
    }

    :global(.editor-content li) {
        @apply text-gray-700 dark:text-gray-300;
    }

    :global(.editor-content a) {
        @apply text-primary underline hover:text-primary-600 dark:hover:text-primary-400;
    }

    :global(.editor-content code) {
        @apply px-1.5 py-0.5 bg-gray-100 dark:bg-gray-800 text-pink-600 dark:text-pink-400 rounded text-sm font-mono;
    }

    :global(.editor-content pre) {
        @apply p-4 bg-gray-900 dark:bg-gray-950 text-gray-100 rounded-lg overflow-x-auto my-4 font-mono text-sm;
    }

    :global(.editor-content hr) {
        @apply border-gray-200 dark:border-gray-700 my-6;
    }

    :global(.editor-content img) {
        @apply max-w-full h-auto rounded-lg my-4;
    }

    /* 툴바 버튼 */
    .toolbar-btn {
        @apply w-7 h-7 flex items-center justify-center rounded text-gray-300 hover:text-white hover:bg-gray-700 transition-colors;
    }

    .toolbar-btn.active {
        @apply text-primary bg-gray-700;
    }
</style>
