<!-- /routes/(apps)/rms-admin/inquiries/[id]/+page.svelte -->
<script>
    import { page } from '$app/stores';
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader } from "$lib/components/ui";

    // URL에서 ID 가져오기
    let inquiryId = $derived($page.params.id);

    // 더미 데이터 (실제로는 API로 가져옴)
    let inquiry = $state({
        inquiryId: 'INQ-2024-0001',
        inquiryDate: '2024-01-20',
        inquirerName: '박지성',
        inquirerEmail: 'js.park@samsung.com',
        inquirerTeam: '삼성전자',
        inquirerPhone: '010-1234-5678',
        title: '기술자료 다운로드 오류 문의',
        content: '안녕하세요.\n\n기술자료 다운로드 시 오류가 발생합니다.\n\n구매한 자료: IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면\n오류 메시지: "파일을 찾을 수 없습니다"\n\n확인 부탁드립니다.\n\n감사합니다.',
        status: 'pending',
        responderName: null,
        responderEmail: null,
        respondedDate: null,
        response: null
    });

    // 답변 내용
    let responseText = $state('');
    let isSubmitting = $state(false);

    // 상태별 스타일
    const statusConfig = {
        answered: {
            label: '답변완료',
            class: 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400'
        },
        pending: {
            label: '답변대기',
            class: 'bg-warning-100 text-warning-700 dark:bg-warning-900 dark:text-warning-400'
        }
    };

    function getStatusConfig(status) {
        return statusConfig[status] || statusConfig.pending;
    }

    // 답변 제출
    async function handleSubmitResponse() {
        if (!responseText.trim()) return;
        
        isSubmitting = true;
        
        // API 호출 시뮬레이션
        await new Promise(resolve => setTimeout(resolve, 1000));
        
        // 상태 업데이트
        inquiry.status = 'answered';
        inquiry.response = responseText;
        inquiry.responderName = '관리자';
        inquiry.responderEmail = 'admin@rms.com';
        inquiry.respondedDate = new Date().toISOString().split('T')[0];
        
        isSubmitting = false;
        
        console.log('Response submitted:', responseText);
    }

    // 목록으로 돌아가기
    function goBack() {
        goto('/rms-admin/inquiries');
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리', href: '/rms-admin/dashboard' },
            { label: '문의사항', href: '/rms-admin/inquiries' },
            { label: inquiry.title }
        ]} 
    />

    <!-- 헤더 -->
    <div class="flex items-center justify-between">
        <div class="flex items-center gap-4">
            <button
                onclick={goBack}
                class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
            >
                <Icon name="ArrowLeft" size="md" />
            </button>
            <div>
                <div class="flex items-center gap-3">
                    <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">{inquiry.title}</h1>
                    <span class="px-2 py-1 text-xs font-medium {getStatusConfig(inquiry.status).class} rounded-full">
                        {getStatusConfig(inquiry.status).label}
                    </span>
                </div>
                <p class="text-gray-500 dark:text-gray-400 mt-1">문의 ID: {inquiry.inquiryId}</p>
            </div>
        </div>
    </div>

    <div class="grid grid-cols-3 gap-6">
        <!-- 문의 내용 (2/3) -->
        <div class="col-span-2 space-y-6">
            <!-- 문의 내용 카드 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">문의 내용</p>
                </header>
                <section class="p-6">
                    <div class="prose dark:prose-invert max-w-none">
                        <p class="text-gray-700 dark:text-gray-300 whitespace-pre-line">{inquiry.content}</p>
                    </div>
                </section>
                <footer class="px-6 py-3 border-t border-gray-100 dark:border-gray-800 bg-gray-50 dark:bg-gray-800">
                    <p class="text-xs text-gray-500 dark:text-gray-400">
                        문의일: {inquiry.inquiryDate}
                    </p>
                </footer>
            </div>

            <!-- 답변 영역 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">
                        {inquiry.status === 'answered' ? '답변 내용' : '답변 작성'}
                    </p>
                </header>
                <section class="p-6">
                    {#if inquiry.status === 'answered'}
                        <!-- 답변 완료된 경우 -->
                        <div class="space-y-4">
                            <div class="p-4 bg-green-50 dark:bg-green-900 border-l-4 border-green-500 rounded-r-lg">
                                <p class="text-gray-700 dark:text-gray-300 whitespace-pre-line">{inquiry.response}</p>
                            </div>
                            <div class="flex items-center justify-between text-sm text-gray-500 dark:text-gray-400">
                                <div class="flex items-center gap-2">
                                    <div class="w-8 h-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 text-sm font-medium">
                                        {inquiry.responderName?.charAt(0) || '?'}
                                    </div>
                                    <span>{inquiry.responderName}</span>
                                    {#if inquiry.responderEmail}
                                        <span class="text-gray-400">({inquiry.responderEmail})</span>
                                    {/if}
                                </div>
                                <span>답변일: {inquiry.respondedDate}</span>
                            </div>
                        </div>
                    {:else}
                        <!-- 답변 대기 중인 경우 -->
                        <div class="space-y-4">
                            <textarea
                                bind:value={responseText}
                                rows="8"
                                placeholder="답변 내용을 입력하세요..."
                                class="w-full px-4 py-3 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                            ></textarea>
                            <div class="flex justify-end">
                                <button
                                    onclick={handleSubmitResponse}
                                    disabled={!responseText.trim() || isSubmitting}
                                    class="px-6 py-2.5 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-2"
                                >
                                    {#if isSubmitting}
                                        <div class="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                                        제출 중...
                                    {:else}
                                        <Icon name="Send" size="sm" />
                                        답변 제출
                                    {/if}
                                </button>
                            </div>
                        </div>
                    {/if}
                </section>
            </div>
        </div>

        <!-- 문의자 정보 사이드바 (1/3) -->
        <div class="space-y-6">
            <!-- 문의자 정보 카드 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">문의자 정보</p>
                </header>
                <section class="p-6 space-y-4">
                    <div class="flex items-center gap-3">
                        <div class="w-12 h-12 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 text-lg font-medium">
                            {inquiry.inquirerName?.charAt(0) || '?'}
                        </div>
                        <div>
                            <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{inquiry.inquirerName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{inquiry.inquirerTeam || '-'}</p>
                        </div>
                    </div>
                    
                    <div class="space-y-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-500 dark:text-gray-400">
                                <Icon name="Mail" size="sm" />
                            </div>
                            <div>
                                <p class="text-xs text-gray-500 dark:text-gray-400">이메일</p>
                                <p class="text-sm text-gray-800 dark:text-gray-200">{inquiry.inquirerEmail}</p>
                            </div>
                        </div>
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-500 dark:text-gray-400">
                                <Icon name="Phone" size="sm" />
                            </div>
                            <div>
                                <p class="text-xs text-gray-500 dark:text-gray-400">전화번호</p>
                                <p class="text-sm text-gray-800 dark:text-gray-200">{inquiry.inquirerPhone || '-'}</p>
                            </div>
                        </div>
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-500 dark:text-gray-400">
                                <Icon name="Building2" size="sm" />
                            </div>
                            <div>
                                <p class="text-xs text-gray-500 dark:text-gray-400">소속</p>
                                <p class="text-sm text-gray-800 dark:text-gray-200">{inquiry.inquirerTeam || '-'}</p>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <!-- 빠른 액션 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">빠른 액션</p>
                </header>
                <section class="p-4 space-y-2">
                    <a
                        href="mailto:{inquiry.inquirerEmail}"
                        class="flex items-center gap-3 px-4 py-3 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        <Icon name="Mail" size="sm" class="text-gray-400" />
                        이메일 보내기
                    </a>
                    <a
                        href="tel:{inquiry.inquirerPhone}"
                        class="flex items-center gap-3 px-4 py-3 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        <Icon name="Phone" size="sm" class="text-gray-400" />
                        전화 걸기
                    </a>
                </section>
            </div>
        </div>
    </div>
</div>
