<!-- /routes/(apps)/rms-admin/faq/[id]/edit/+page.svelte -->
<script>
    import { page } from '$app/stores';
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb } from "$lib/components/ui";

    // URL에서 ID 가져오기
    let faqId = $derived($page.params.id);

    // 더미 데이터 (실제로는 API로 가져옴)
    let formData = $state({
        question: '기술자료는 어떻게 구매하나요?',
        answer: '원하는 기술자료를 선택한 후 결제하시면 즉시 다운로드 가능합니다.',
        isPublished: true
    });

    let isSubmitting = $state(false);

    // 유효성 검사
    let isValid = $derived(
        formData.question.trim() !== '' &&
        formData.answer.trim() !== ''
    );

    // 폼 제출
    async function handleSubmit() {
        if (!isValid) return;
        
        isSubmitting = true;
        
        // API 호출 시뮬레이션
        await new Promise(resolve => setTimeout(resolve, 1000));
        
        console.log('Updated:', faqId, formData);
        
        isSubmitting = false;
        
        // 목록으로 이동
        goto('/rms-admin/faq');
    }

    // 취소
    function handleCancel() {
        goto('/rms-admin/faq');
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리', href: '/rms-admin/dashboard' },
            { label: 'FAQ 관리', href: '/rms-admin/faq' },
            { label: 'FAQ 수정' }
        ]} 
    />

    <!-- 헤더 -->
    <div class="flex items-center gap-4">
        <button
            onclick={handleCancel}
            class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            <Icon name="ArrowLeft" size="md" />
        </button>
        <div>
            <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">FAQ 수정</h1>
            <p class="text-gray-500 dark:text-gray-400 mt-1">FAQ ID: {faqId}</p>
        </div>
    </div>

    <!-- 폼 -->
    <div class="max-w-3xl">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
            <section class="p-6 space-y-6">
                <!-- 질문 -->
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                        질문 <span class="text-danger">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.question}
                        placeholder="자주 묻는 질문을 입력하세요"
                        class="w-full px-4 py-3 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                    />
                </div>

                <!-- 답변 -->
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                        답변 <span class="text-danger">*</span>
                    </label>
                    <textarea
                        bind:value={formData.answer}
                        rows="8"
                        placeholder="질문에 대한 답변을 입력하세요"
                        class="w-full px-4 py-3 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                    ></textarea>
                </div>

                <!-- 게시 상태 -->
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                        게시 상태
                    </label>
                    <div class="flex items-center gap-4">
                        <label class="flex items-center gap-2 cursor-pointer">
                            <input
                                type="radio"
                                name="isPublished"
                                checked={formData.isPublished === true}
                                onchange={() => formData.isPublished = true}
                                class="w-4 h-4 text-primary border-gray-300 focus:ring-primary"
                            />
                            <span class="text-sm text-gray-700 dark:text-gray-300">게시</span>
                        </label>
                        <label class="flex items-center gap-2 cursor-pointer">
                            <input
                                type="radio"
                                name="isPublished"
                                checked={formData.isPublished === false}
                                onchange={() => formData.isPublished = false}
                                class="w-4 h-4 text-primary border-gray-300 focus:ring-primary"
                            />
                            <span class="text-sm text-gray-700 dark:text-gray-300">비공개</span>
                        </label>
                    </div>
                </div>
            </section>

            <!-- 버튼 영역 -->
            <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800 bg-gray-50 dark:bg-gray-800 rounded-b-xl flex justify-end gap-3">
                <button
                    onclick={handleCancel}
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-200 dark:hover:bg-gray-700 rounded-lg transition-colors"
                >
                    취소
                </button>
                <button
                    onclick={handleSubmit}
                    disabled={!isValid || isSubmitting}
                    class="px-6 py-2 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-2"
                >
                    {#if isSubmitting}
                        <div class="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                        저장 중...
                    {:else}
                        <Icon name="Check" size="sm" />
                        저장
                    {/if}
                </button>
            </footer>
        </div>
    </div>
</div>
