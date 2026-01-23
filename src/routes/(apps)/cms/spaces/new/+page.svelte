<script>
    /**
     * CMS - Space 생성 페이지
     */
    import Icon from "$lib/icons/icon.svelte";

    let formData = $state({
        name: '',
        description: '',
        type: 'Open',
    });

    const typeOptions = [
        { value: 'Open', label: 'Open', desc: '모든 인증 사용자가 작성/발행 가능' },
        { value: 'Moderated', label: 'Moderated', desc: '모든 사용자 작성 가능, Editor만 발행' },
        { value: 'Closed', label: 'Closed', desc: 'Space 멤버만 작성/발행 가능' },
    ];

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Create space:', formData);
    }
</script>

<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/apps/cms/spaces"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Space 추가</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">새 콘텐츠 공간을 생성합니다</p>
                </div>
            </div>
        </header>

        <form onsubmit={handleSubmit} class="p-6 space-y-6">
            <div class="space-y-4">
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        Space 이름 <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.name}
                        placeholder="Tech Blog"
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
                        placeholder="이 Space의 목적을 설명하세요"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                    ></textarea>
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        타입 <span class="text-red-500">*</span>
                    </label>
                    <div class="space-y-2">
                        {#each typeOptions as option}
                            <label class="flex items-start gap-3 p-3 border border-gray-200 dark:border-gray-700 rounded-lg cursor-pointer hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors {formData.type === option.value ? 'border-primary bg-primary-50 dark:bg-primary-900/20' : ''}">
                                <input
                                    type="radio"
                                    name="type"
                                    value={option.value}
                                    bind:group={formData.type}
                                    class="mt-0.5"
                                />
                                <div>
                                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{option.label}</p>
                                    <p class="text-xs text-gray-500 dark:text-gray-400">{option.desc}</p>
                                </div>
                            </label>
                        {/each}
                    </div>
                </div>
            </div>

            <div class="flex items-center justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                <a
                    href="/apps/cms/spaces"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </a>
                <button
                    type="submit"
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                    Space 생성
                </button>
            </div>
        </form>
    </div>
</div>
