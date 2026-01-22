<script>
    /**
     * 권한 관리 - 수정 페이지
     */
    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    let permissionId = $derived($page.params.id);

    // 샘플 데이터 (실제로는 API에서 조회)
    let formData = $state({
        code: 'user:read',
        name: '사용자 조회',
        category: 'user',
        description: '사용자 목록 및 상세 정보를 조회할 수 있습니다.',
    });

    const categoryOptions = ['user', 'asset', 'team', 'admin'];

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Update permission:', permissionId, formData);
    }

    function handleDelete() {
        if (confirm('정말 이 권한을 삭제하시겠습니까?')) {
            console.log('Delete permission:', permissionId);
        }
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
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">권한 수정</p>
                    <code class="text-sm text-gray-500 dark:text-gray-400 font-mono">{formData.code}</code>
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
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors font-mono"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            권한명 <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="text"
                            bind:value={formData.name}
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
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                    ></textarea>
                </div>
            </div>

            <!-- 버튼 -->
            <div class="flex items-center justify-between pt-4 border-t border-gray-100 dark:border-gray-800">
                <button
                    type="button"
                    onclick={handleDelete}
                    class="px-4 py-2 text-sm font-medium text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                >
                    권한 삭제
                </button>
                <div class="flex items-center gap-3">
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
                        저장
                    </button>
                </div>
            </div>
        </form>
    </div>
</div>
