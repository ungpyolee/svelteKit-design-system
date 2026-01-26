<!-- $lib/components/ui/editor/classic-editor.svelte -->
<script>
    import { onMount, onDestroy } from 'svelte';
    
    let {
        value = '',
        placeholder = '내용을 입력하세요...',
        onChange = () => {}
    } = $props();
    
    let editorContainer;
    let quill = $state(null);

    onMount(async () => {
        const Quill = (await import('quill')).default;
        await import('quill/dist/quill.snow.css');

        quill = new Quill(editorContainer, {
            theme: 'snow',
            placeholder,
            modules: {
                toolbar: [
                    [{ 'header': [1, 2, 3, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    [{ 'color': [] }, { 'background': [] }],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    [{ 'align': [] }],
                    ['link', 'image'],
                    ['clean']
                ]
            }
        });

        if (value) {
            quill.root.innerHTML = value;
        }

        quill.on('text-change', () => {
            onChange(quill.root.innerHTML);
        });
    });

    onDestroy(() => {
        quill = null;
    });

    export function getHTML() {
        return quill?.root.innerHTML || '';
    }

    export function getText() {
        return quill?.getText() || '';
    }

    export function setHTML(html) {
        if (quill) quill.root.innerHTML = html;
    }
</script>

<div class="quill-editor">
    <div bind:this={editorContainer}></div>
</div>

<style>
    .quill-editor {
        @apply overflow-hidden;
    }

    /* 툴바 */
    :global(.ql-toolbar.ql-snow) {
        @apply border-0 border-b border-gray-100 dark:border-gray-800 
               bg-gray-50 dark:bg-gray-800 
               px-4 py-2 flex flex-wrap items-center gap-1;
    }

    /* 툴바 그룹 */
    :global(.ql-toolbar.ql-snow .ql-formats) {
        @apply flex items-center mr-3 pr-3 border-r border-gray-200 dark:border-gray-700;
        margin-top: 0 !important;
        margin-bottom: 0 !important;
    }

    :global(.ql-toolbar.ql-snow .ql-formats:last-child) {
        @apply mr-0 pr-0 border-r-0;
    }

    /* 툴바 버튼 */
    :global(.ql-toolbar.ql-snow button) {
        @apply w-8 h-8 p-1.5 rounded-md transition-colors flex items-center justify-center;
        float: none !important;
    }

    :global(.ql-toolbar.ql-snow button:hover) {
        @apply bg-gray-200 dark:bg-gray-700;
    }

    :global(.ql-toolbar.ql-snow button.ql-active) {
        @apply bg-blue-100 dark:bg-blue-900;
    }

    /* 툴바 버튼 내 SVG */
    :global(.ql-toolbar.ql-snow button svg) {
        @apply w-4 h-4;
        float: none !important;
        margin: 0 !important;
    }

    /* 툴바 드롭다운 */
    :global(.ql-toolbar.ql-snow .ql-picker) {
        @apply h-8 flex items-center;
        float: none !important;
    }

    :global(.ql-toolbar.ql-snow .ql-picker-label) {
        @apply border border-transparent rounded-md px-2 h-8 flex items-center
               text-sm text-gray-600 dark:text-gray-400
               hover:bg-gray-200 dark:hover:bg-gray-700 transition-colors;
        padding-top: 0 !important;
        padding-bottom: 0 !important;
    }

    :global(.ql-toolbar.ql-snow .ql-picker.ql-expanded .ql-picker-label) {
        @apply border-gray-200 dark:border-gray-700 bg-gray-100 dark:bg-gray-700;
    }

    /* 드롭다운 메뉴 */
    :global(.ql-toolbar.ql-snow .ql-picker-options) {
        @apply border border-gray-200 dark:border-gray-700 
               bg-white dark:bg-gray-800 
               rounded-lg shadow-lg mt-1 p-1;
    }

    :global(.ql-toolbar.ql-snow .ql-picker-item) {
        @apply px-3 py-1.5 rounded-md text-sm
               text-gray-700 dark:text-gray-300
               hover:bg-gray-100 dark:hover:bg-gray-700;
    }

    /* 에디터 본문 영역 */
    :global(.ql-container.ql-snow) {
        @apply border-0 bg-white dark:bg-gray-900;
    }

    :global(.ql-editor) {
        @apply text-gray-900 dark:text-gray-100 
               text-base leading-relaxed
               min-h-[300px] px-6 py-4;
    }

    :global(.ql-editor.ql-blank::before) {
        @apply text-gray-400 dark:text-gray-500 not-italic left-6;
    }

    /* 아이콘 색상 - 라이트모드 */
    :global(.ql-toolbar .ql-stroke) {
        stroke: #6b7280;
    }

    :global(.ql-toolbar .ql-fill) {
        fill: #6b7280;
    }

    :global(.ql-toolbar button:hover .ql-stroke) {
        stroke: #374151;
    }

    :global(.ql-toolbar button:hover .ql-fill) {
        fill: #374151;
    }

    :global(.ql-toolbar button.ql-active .ql-stroke) {
        stroke: #2563eb;
    }

    :global(.ql-toolbar button.ql-active .ql-fill) {
        fill: #2563eb;
    }

    /* 아이콘 색상 - 다크모드 */
    :global(.dark .ql-toolbar .ql-stroke) {
        stroke: #9ca3af;
    }

    :global(.dark .ql-toolbar .ql-fill) {
        fill: #9ca3af;
    }

    :global(.dark .ql-toolbar button:hover .ql-stroke) {
        stroke: #e5e7eb;
    }

    :global(.dark .ql-toolbar button:hover .ql-fill) {
        fill: #e5e7eb;
    }

    :global(.dark .ql-toolbar button.ql-active .ql-stroke) {
        stroke: #3b82f6;
    }

    :global(.dark .ql-toolbar button.ql-active .ql-fill) {
        fill: #3b82f6;
    }

    /* 드롭다운 화살표 */
    :global(.ql-toolbar .ql-picker-label::before) {
        @apply text-gray-600 dark:text-gray-400;
    }

    :global(.dark .ql-toolbar .ql-picker-label .ql-stroke) {
        stroke: #9ca3af;
    }

    /* 컬러 피커 */
    :global(.ql-color-picker .ql-picker-options),
    :global(.ql-background .ql-picker-options) {
        @apply p-2 w-auto;
    }

    :global(.ql-color-picker .ql-picker-item),
    :global(.ql-background .ql-picker-item) {
        @apply w-6 h-6 rounded border border-gray-200 dark:border-gray-600 m-0.5;
    }

    /* 링크 툴팁 */
    :global(.ql-tooltip) {
        @apply bg-white dark:bg-gray-800 
               border border-gray-200 dark:border-gray-700 
               rounded-lg shadow-lg px-3 py-2 z-50;
    }

    :global(.ql-tooltip input[type="text"]) {
        @apply border border-gray-200 dark:border-gray-700 
               bg-white dark:bg-gray-900 
               text-gray-900 dark:text-gray-100
               rounded-md px-3 py-1.5 text-sm outline-none
               focus:border-blue-500 focus:ring-1 focus:ring-blue-500;
    }

    :global(.ql-tooltip a.ql-action),
    :global(.ql-tooltip a.ql-remove) {
        @apply text-sm text-blue-600 dark:text-blue-400 hover:text-blue-700 dark:hover:text-blue-300 ml-2;
    }

    /* 스크롤바 */
    :global(.ql-editor::-webkit-scrollbar) {
        @apply w-2;
    }

    :global(.ql-editor::-webkit-scrollbar-track) {
        @apply bg-transparent;
    }

    :global(.ql-editor::-webkit-scrollbar-thumb) {
        @apply bg-gray-300 dark:bg-gray-600 rounded-full;
    }

    :global(.ql-editor::-webkit-scrollbar-thumb:hover) {
        @apply bg-gray-400 dark:bg-gray-500;
    }
</style>