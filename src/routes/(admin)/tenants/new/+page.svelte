<script>
    /**
     * 테넌트 관리 - 생성 페이지
     */
    import Icon from "$lib/icons/icon.svelte";

    let formData = $state({
        name: '',
        slug: '',
    });

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Create tenant:', formData);
    }

    // slug 자동 생성 (name 입력 시)
    function generateSlug(name) {
        return name.toLowerCase().replace(/[^a-z0-9]+/g, '-').replace(/(^-|-$)/g, '');
    }
</script>

<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/admin/tenants"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">테넌트 추가</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">새 테넌트(팀/조직)를 생성합니다</p>
                </div>
            </div>
        </header>

        <form onsubmit={handleSubmit} class="p-6 space-y-6">
            <div class="space-y-4">
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        테넌트 이름 <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.name}
                        oninput={() => formData.slug = generateSlug(formData.name)}
                        placeholder="CLEW"
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        Slug <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.slug}
                        placeholder="clew"
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors font-mono"
                    />
                    <p class="mt-1 text-xs text-gray-500">URL에 사용됩니다. 영문 소문자, 숫자, 하이픈만 가능</p>
                </div>
            </div>

            <div class="flex items-center justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                <a
                    href="/admin/tenants"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </a>
                <button
                    type="submit"
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                    테넌트 생성
                </button>
            </div>
        </form>
    </div>
</div>
