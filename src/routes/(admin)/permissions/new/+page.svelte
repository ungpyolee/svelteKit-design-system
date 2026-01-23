<script>
    /**
     * 권한 관리 - 생성 페이지
     */
    import Icon from "$lib/icons/icon.svelte";

    let formData = $state({
        code: '',
        name: '',
        category: '',
        description: '',
    });

    // 카테고리 옵션 (실제로는 API에서 가져오거나 설정)
    const categoryOptions = ['user', 'asset', 'team', 'admin'];

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Create permission:', formData);
    }
</script>

<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <!-- 헤더 -->
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/admin/permissions"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">권한 추가</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">새 시스템 권한을 생성합니다</p>
                </div>
            </div>
        </header>

        <!-- 폼 -->
        <form onsubmit={handleSubmit} class="p-6 space-y-6">
            <div class="space-y-4">
                <!-- 코드 + 이름 -->
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            코드 <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="text"
                            bind:value={formData.code}
                            placeholder="user:read"
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors font-mono"
                        />
                        <p class="mt-1 text-xs text-gray-500">예: user:read, asset:write</p>
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            권한명 <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="text"
                            bind:value={formData.name}
                            placeholder="사용자 조회"
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                </div>

                <!-- 카테고리 -->
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        카테고리 <span class="text-red-500">*</span>
                    </label>
                    <select
                        bind:value={formData.category}
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    >
                        <option value="">카테고리 선택</option>
                        {#each categoryOptions as category}
                            <option value={category}>{category}</option>
                        {/each}
                    </select>
                </div>

                <!-- 설명 -->
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        설명
                    </label>
                    <textarea
                        bind:value={formData.description}
                        rows="3"
                        placeholder="이 권한에 대한 설명을 입력하세요"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                    ></textarea>
                </div>
            </div>

            <!-- 버튼 -->
            <div class="flex items-center justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                <a
                    href="/admin/permissions"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </a>
                <button
                    type="submit"
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                    권한 추가
                </button>
            </div>
        </form>
    </div>
</div>
