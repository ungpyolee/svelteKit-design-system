<script>
    /**
     * CMS - 토픽 생성 페이지
     * Space 선택 필수
     */
    import Icon from "$lib/icons/icon.svelte";

    // Space 목록
    let spaces = $state([
        { id: 1, name: 'Tech Blog' },
        { id: 2, name: 'Product Docs' },
        { id: 3, name: 'Internal Wiki' },
    ]);

    let formData = $state({
        spaceId: '',
        name: '',
        description: '',
    });

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Create topic:', formData);
    }
</script>

<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/apps/cms/topics"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">새 토픽</p>
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
                        <option value="">Space 선택</option>
                        {#each spaces as space}
                            <option value={space.id}>{space.name}</option>
                        {/each}
                    </select>
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        토픽명 <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.name}
                        required
                        placeholder="예: 프론트엔드"
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
                        placeholder="토픽에 대한 설명"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                    ></textarea>
                </div>
            </div>

            <div class="flex items-center justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                <a
                    href="/apps/cms/topics"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </a>
                <button
                    type="submit"
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                    생성
                </button>
            </div>
        </form>
    </div>
</div>
