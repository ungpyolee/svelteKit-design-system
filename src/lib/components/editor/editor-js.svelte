<!-- $lib/components/ui/editor/editor.svelte -->
<script>
    import { onMount, onDestroy } from 'svelte';
    
    /** @type {{
     *   data?: object,
     *   placeholder?: string,
     *   readOnly?: boolean,
     *   onChange?: (data: object) => void
     * }} 
     */
    let { 
        data = undefined,
        placeholder = '내용을 입력하세요...',
        readOnly = false,
        onChange = () => {}
    } = $props();
    
    /** @type {object|null} */
    let editor = $state(null);
    
    /** @type {HTMLElement} */
    let editorElement;

    onMount(async () => {
        const EditorJS = (await import('@editorjs/editorjs')).default;
        const Header = (await import('@editorjs/header')).default;
        const List = (await import('@editorjs/list')).default;
        const ImageTool = (await import('@editorjs/image')).default;
        const Quote = (await import('@editorjs/quote')).default;
        const Code = (await import('@editorjs/code')).default;
        const Delimiter = (await import('@editorjs/delimiter')).default;
        const Embed = (await import('@editorjs/embed')).default;
        const Table = (await import('@editorjs/table')).default;
        const Marker = (await import('@editorjs/marker')).default;
        const InlineCode = (await import('@editorjs/inline-code')).default;

        editor = new EditorJS({
            holder: editorElement,
            placeholder,
            readOnly,
            data,
            tools: {
                header: {
                    class: Header,
                    config: {
                        levels: [2, 3, 4],
                        defaultLevel: 2
                    }
                },
                list: {
                    class: List,
                    inlineToolbar: true,
                    config: {
                        defaultStyle: 'unordered'
                    }
                },
                image: {
                    class: ImageTool,
                    config: {
                        uploader: {
                            async uploadByFile(file) {
                                return new Promise((resolve) => {
                                    const reader = new FileReader();
                                    reader.onload = (e) => {
                                        resolve({
                                            success: 1,
                                            file: { url: e.target.result }
                                        });
                                    };
                                    reader.readAsDataURL(file);
                                });
                            },
                            async uploadByUrl(url) {
                                return { success: 1, file: { url } };
                            }
                        }
                    }
                },
                quote: {
                    class: Quote,
                    inlineToolbar: true,
                    config: {
                        quotePlaceholder: '인용문을 입력하세요',
                        captionPlaceholder: '출처를 입력하세요'
                    }
                },
                code: Code,
                delimiter: Delimiter,
                embed: {
                    class: Embed,
                    config: {
                        services: { youtube: true, vimeo: true }
                    }
                },
                table: {
                    class: Table,
                    inlineToolbar: true
                },
                marker: Marker,
                inlineCode: InlineCode
            },
            onChange: async () => {
                if (editor) {
                    const savedData = await editor.save();
                    onChange(savedData);
                }
            }
        });
    });

    onDestroy(() => {
        if (editor && typeof editor.destroy === 'function') {
            editor.destroy();
        }
    });

    // 외부에서 호출 가능한 메서드들
    export async function save() {
        if (editor) return await editor.save();
        return null;
    }

    export async function render(newData) {
        if (editor) await editor.render(newData);
    }

    export async function clear() {
        if (editor) await editor.clear();
    }
</script>

<div 
    bind:this={editorElement} 
    class="editor-container min-h-[300px] [&_.codex-editor]:font-sans"
></div>

<!-- Tailwind로 변환 불가능한 Editor.js 내부 스타일만 유지 -->
<style>
    /* Editor.js 레이아웃 - 라이브러리 내부 클래스라 Tailwind 적용 불가 */
    :global(.codex-editor__redactor) {
        padding-bottom: 100px !important;
    }

    :global(.ce-block__content),
    :global(.ce-toolbar__content) {
        max-width: 100% !important;
    }

    /* 다크모드 - 툴바 */
    :global(.dark .ce-toolbar__plus),
    :global(.dark .ce-toolbar__settings-btn) {
        @apply text-gray-400 bg-gray-700;
    }

    :global(.dark .ce-toolbar__plus:hover),
    :global(.dark .ce-toolbar__settings-btn:hover) {
        @apply bg-gray-600;
    }

    /* 다크모드 - 팝오버/툴박스 */
    :global(.dark .ce-inline-toolbar),
    :global(.dark .ce-conversion-toolbar),
    :global(.dark .ce-settings),
    :global(.dark .ce-toolbox),
    :global(.dark .ce-popover) {
        @apply bg-gray-800 border-gray-700 text-gray-200;
    }

    :global(.dark .ce-inline-tool),
    :global(.dark .ce-conversion-tool),
    :global(.dark .ce-settings__button),
    :global(.dark .ce-toolbox__button) {
        @apply text-gray-200;
    }

    :global(.dark .ce-inline-tool:hover),
    :global(.dark .ce-conversion-tool:hover),
    :global(.dark .ce-settings__button:hover),
    :global(.dark .ce-toolbox__button:hover),
    :global(.dark .ce-popover-item:hover) {
        @apply bg-gray-700;
    }

    :global(.dark .ce-popover-item__title) {
        @apply text-gray-200;
    }

    /* 다크모드 - 입력 필드 */
    :global(.dark .cdx-input) {
        @apply bg-gray-700 border-gray-600 text-gray-200;
    }

    /* 다크모드 - 콘텐츠 블록 */
    :global(.dark .ce-paragraph),
    :global(.dark .ce-header) {
        @apply text-gray-200;
    }

    :global(.dark .ce-block--selected .ce-block__content) {
        @apply bg-blue-500/10;
    }

    /* 다크모드 - 인용문 */
    :global(.dark .cdx-quote__text) {
        @apply text-gray-200 border-l-gray-500;
    }

    :global(.dark .cdx-quote__caption) {
        @apply text-gray-400;
    }

    /* 다크모드 - 코드 블록 */
    :global(.dark .ce-code__textarea) {
        @apply bg-gray-900 text-gray-200 border-gray-700;
    }

    /* 다크모드 - 테이블 */
    :global(.dark .tc-table),
    :global(.dark .tc-row),
    :global(.dark .tc-cell) {
        @apply border-gray-700;
    }

    :global(.dark .tc-cell) {
        @apply bg-gray-800 text-gray-200;
    }

    :global(.dark .tc-toolbox__toggler) {
        @apply text-gray-400;
    }

    :global(.dark .tc-popover) {
        @apply bg-gray-800 border-gray-700;
    }
</style>