<script>
    /**
     * CMS - 콘텐츠 작성 페이지
     * EditorJS 기반 에디터 영역 포함
     */
    import Icon from "$lib/icons/icon.svelte";

    let formData = $state({
        title: '',
        summary: '',
        slug: '',
        spaceId: '',
        status: 'Draft',
        coverImageUrl: '',
        lang: 'ko',
    });

    // Space 목록
    let spaces = $state([
        { id: 1, name: 'Tech Blog' },
        { id: 2, name: 'Product Docs' },
        { id: 3, name: 'Internal Wiki' },
    ]);

    const statusOptions = [
        { value: 'Draft', label: 'Draft - 임시저장' },
        { value: 'Published', label: 'Published - 발행' },
    ];

    const langOptions = [
        { value: 'ko', label: '한국어' },
        { value: 'en', label: 'English' },
        { value: 'ja', label: '日本語' },
    ];

    function generateSlug(title) {
        return title.toLowerCase()
            .replace(/[가-힣]/g, '')
            .replace(/[^a-z0-9]+/g, '-')
            .replace(/(^-|-$)/g, '');
    }

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Create content:', formData);
    }
</script>

<div class="max-w-5xl mx-auto space-y-6">
    <!-- 헤더 -->
    <div class="flex items-center justify-between">
        <div class="flex items-center gap-3">
            <a
                href="/apps/cms/contents"
                class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
            >
                <Icon name="ArrowLeft" size="sm" />
            </a>
            <div>
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">새 콘텐츠 작성</p>
            </div>
        </div>
        <div class="flex items-center gap-2">
            <button
                type="button"
                class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
            >
                임시저장
            </button>
            <button
                onclick={handleSubmit}
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                발행
            </button>
        </div>
    </div>

    <div class="grid grid-cols-3 gap-6">
        <!-- 메인 에디터 영역 (2/3) -->
        <div class="col-span-2 space-y-6">
            <!-- 제목 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <input
                    type="text"
                    bind:value={formData.title}
                    oninput={() => formData.slug = generateSlug(formData.title)}
                    placeholder="제목을 입력하세요"
                    class="w-full text-2xl font-bold text-gray-900 dark:text-gray-100 bg-transparent border-none outline-none placeholder:text-gray-400"
                />
                <div class="mt-2 flex items-center gap-2 text-sm text-gray-500">
                    <span>slug:</span>
                    <input
                        type="text"
                        bind:value={formData.slug}
                        placeholder="url-slug"
                        class="flex-1 px-2 py-1 bg-gray-50 dark:bg-gray-800 rounded text-sm font-mono"
                    />
                </div>
            </div>

            <!-- 요약 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">요약</label>
                <textarea
                    bind:value={formData.summary}
                    rows="2"
                    placeholder="콘텐츠 요약을 입력하세요 (목록에 표시됩니다)"
                    class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                ></textarea>
            </div>

            <!-- EditorJS 영역 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-4">본문</label>
                <!-- EditorJS가 마운트될 영역 -->
                <div class="min-h-[400px] border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-4">
                    <div class="text-center text-gray-500 dark:text-gray-400">
                        <Icon name="FileEdit" size="lg" class="mx-auto mb-2 opacity-50"/>
                        <p class="text-sm">EditorJS 에디터 영역</p>
                        <p class="text-xs mt-1">실제 구현 시 EditorJS 컴포넌트가 마운트됩니다</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- 사이드바 (1/3) -->
        <div class="space-y-6">
            <!-- 발행 설정 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-4">발행 설정</h3>
                <div class="space-y-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">Space</label>
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
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">상태</label>
                        <select
                            bind:value={formData.status}
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        >
                            {#each statusOptions as option}
                                <option value={option.value}>{option.label}</option>
                            {/each}
                        </select>
                    </div>

                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">언어</label>
                        <select
                            bind:value={formData.lang}
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        >
                            {#each langOptions as option}
                                <option value={option.value}>{option.label}</option>
                            {/each}
                        </select>
                    </div>
                </div>
            </div>

            <!-- 커버 이미지 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-4">커버 이미지</h3>
                <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-4 text-center">
                    <Icon name="Image" size="lg" class="mx-auto mb-2 text-gray-400"/>
                    <p class="text-sm text-gray-500 dark:text-gray-400">이미지를 드래그하거나</p>
                    <button class="mt-2 px-3 py-1.5 text-sm text-primary border border-primary rounded-lg hover:bg-primary-50 dark:hover:bg-primary-900/20 transition-colors">
                        파일 선택
                    </button>
                </div>
            </div>

            <!-- 태그 & 토픽 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-4">태그 & 토픽</h3>
                <div class="space-y-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">태그</label>
                        <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                            <p class="text-xs text-gray-500 text-center">태그 선택 UI (TODO)</p>
                        </div>
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">토픽</label>
                        <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                            <p class="text-xs text-gray-500 text-center">토픽 선택 UI (TODO)</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
