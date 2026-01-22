<script>
    /**
     * CMS - 태그 수정 페이지
     */
    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    let tagId = $derived($page.params.id);

    // Space 목록
    let spaces = $state([
        { id: 1, name: 'Tech Blog' },
        { id: 2, name: 'Product Docs' },
        { id: 3, name: 'Internal Wiki' },
    ]);

    // 샘플 데이터
    let formData = $state({
        spaceId: 1,
        name: 'Svelte',
        description: 'Svelte 관련 콘텐츠',
    });

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Update tag:', tagId, formData);
    }

    function handleDelete() {
        if (confirm('정말 이 태그를 삭제하시겠습니까?')) {
            console.log('Delete tag:', tagId);
        }
    }
</script>

<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/apps/cms/tags"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">태그 수정</p>
                    <p class="text-sm text-gray-500">ID: {tagId}</p>
                </div>
            </div>
        </header>

        <form onsubmit={handleSubmit} class="p-6 space-y-6">
            <div class="space-y-4">
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        Space <span class="text-red-500">*</span>
                    </label>
                    <select
                        bind:value={formData.spaceId}
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    >
                        {#each spaces as space}
                            <option value={space.id}>{space.name}</option>
                        {/each}
                    </select>
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        태그명 <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.name}
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        설명
                    </label>
                    <textarea
                        bind:value={formData.description}
                        rows="3"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                    ></textarea>
                </div>
            </div>

            <div class="flex items-center justify-between pt-4 border-t border-gray-100 dark:border-gray-800">
                <button
                    type="button"
                    onclick={handleDelete}
                    class="px-4 py-2 text-sm font-medium text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                >
                    삭제
                </button>
                <div class="flex items-center gap-3">
                    <a
                        href="/apps/cms/tags"
                        class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        취소
                    </a>
                    <button
                        type="submit"
                        class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                    >
                        저장
                    </button>
                </div>
            </div>
        </form>
    </div>
</div>
