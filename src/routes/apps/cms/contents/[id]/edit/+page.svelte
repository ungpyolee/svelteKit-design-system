<script>
    /**
     * CMS - 콘텐츠 편집 페이지
     */
    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    let contentId = $derived($page.params.id);

    // 샘플 데이터
    let formData = $state({
        title: 'SvelteKit 5 새로운 기능 소개',
        summary: 'SvelteKit 5에서 추가된 주요 기능들을 살펴봅니다.',
        slug: 'sveltekit-5-new-features',
        spaceId: 1,
        status: 'Published',
        coverImageUrl: '',
        lang: 'ko',
        viewCount: 1234,
        createdAt: '2024-01-10T09:00:00.000Z',
        publishedAt: '2024-01-15T09:00:00.000Z',
    });

    let spaces = $state([
        { id: 1, name: 'Tech Blog' },
        { id: 2, name: 'Product Docs' },
        { id: 3, name: 'Internal Wiki' },
    ]);

    const statusOptions = [
        { value: 'Draft', label: 'Draft - 임시저장' },
        { value: 'Published', label: 'Published - 발행' },
        { value: 'Archived', label: 'Archived - 보관' },
    ];

    const langOptions = [
        { value: 'ko', label: '한국어' },
        { value: 'en', label: 'English' },
        { value: 'ja', label: '日本語' },
    ];

    function formatDate(dateString) {
        if (!dateString) return '-';
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    }

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Update content:', contentId, formData);
    }

    function handleDelete() {
        if (confirm('정말 이 콘텐츠를 삭제하시겠습니까?')) {
            console.log('Delete content:', contentId);
        }
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
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">콘텐츠 편집</p>
                <p class="text-sm text-gray-500">ID: {contentId}</p>
            </div>
        </div>
        <div class="flex items-center gap-2">
            <button
                type="button"
                onclick={handleDelete}
                class="px-4 py-2 text-sm font-medium text-red-600 border border-red-200 dark:border-red-800 rounded-lg hover:bg-red-50 dark:hover:bg-red-900/20 transition-colors"
            >
                삭제
            </button>
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
                저장
            </button>
        </div>
    </div>

    <div class="grid grid-cols-3 gap-6">
        <!-- 메인 에디터 영역 -->
        <div class="col-span-2 space-y-6">
            <!-- 제목 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <input
                    type="text"
                    bind:value={formData.title}
                    placeholder="제목을 입력하세요"
                    class="w-full text-2xl font-bold text-gray-900 dark:text-gray-100 bg-transparent border-none outline-none placeholder:text-gray-400"
                />
                <div class="mt-2 flex items-center gap-2 text-sm text-gray-500">
                    <span>slug:</span>
                    <input
                        type="text"
                        bind:value={formData.slug}
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
                    class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                ></textarea>
            </div>

            <!-- EditorJS 영역 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-4">본문</label>
                <div class="min-h-[400px] border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-4">
                    <div class="text-center text-gray-500 dark:text-gray-400">
                        <Icon name="FileEdit" size="lg" class="mx-auto mb-2 opacity-50"/>
                        <p class="text-sm">EditorJS 에디터 영역</p>
                        <p class="text-xs mt-1">기존 BodyContent(JSON) 로드 후 편집</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- 사이드바 -->
        <div class="space-y-6">
            <!-- 발행 정보 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-4">발행 정보</h3>
                <div class="space-y-3 text-sm">
                    <div class="flex justify-between">
                        <span class="text-gray-500">조회수</span>
                        <span class="font-medium text-gray-800 dark:text-gray-200">{formData.viewCount.toLocaleString()}</span>
                    </div>
                    <div class="flex justify-between">
                        <span class="text-gray-500">생성일</span>
                        <span class="text-gray-800 dark:text-gray-200">{formatDate(formData.createdAt)}</span>
                    </div>
                    <div class="flex justify-between">
                        <span class="text-gray-500">발행일</span>
                        <span class="text-gray-800 dark:text-gray-200">{formatDate(formData.publishedAt)}</span>
                    </div>
                </div>
            </div>

            <!-- 발행 설정 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-4">발행 설정</h3>
                <div class="space-y-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">Space</label>
                        <select
                            bind:value={formData.spaceId}
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        >
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
                    <button class="px-3 py-1.5 text-sm text-primary border border-primary rounded-lg hover:bg-primary-50 dark:hover:bg-primary-900/20 transition-colors">
                        이미지 변경
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
