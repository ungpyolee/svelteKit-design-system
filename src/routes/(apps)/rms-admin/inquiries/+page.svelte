<!-- /routes/(apps)/rms-admin/inquiries/+page.svelte -->
<script>
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import InquiryTable from "../InquiryTable.svelte";

    // 더미 데이터
    let inquiries = $state([
        {
            inquiryId: 'INQ-2024-0001',
            inquiryDate: '2024-01-20',
            inquirerName: '박지성',
            inquirerEmail: 'js.park@samsung.com',
            inquirerTeam: '삼성전자',
            inquirerPhone: '010-1234-5678',
            title: '기술자료 다운로드 오류 문의',
            content: '기술자료 다운로드 시 오류가 발생합니다. 확인 부탁드립니다.',
            status: 'pending',
            responderName: null,
            responderEmail: null,
            respondedDate: null,
            response: null
        },
        {
            inquiryId: 'INQ-2024-0002',
            inquiryDate: '2024-01-19',
            inquirerName: '김영수',
            inquirerEmail: 'ys.kim@hyundai.com',
            inquirerTeam: '현대자동차',
            inquirerPhone: '010-2345-6789',
            title: '결제 관련 문의',
            content: '결제 후 기술자료가 보이지 않습니다.',
            status: 'pending',
            responderName: null,
            responderEmail: null,
            respondedDate: null,
            response: null
        },
        {
            inquiryId: 'INQ-2024-0003',
            inquiryDate: '2024-01-18',
            inquirerName: '이준호',
            inquirerEmail: 'jh.lee@lg.com',
            inquirerTeam: 'LG전자',
            inquirerPhone: '010-3456-7890',
            title: '기술자료 등록 절차 문의',
            content: '기여자로 등록하려면 어떤 절차가 필요한가요?',
            status: 'answered',
            responderName: '관리자',
            responderEmail: 'admin@rms.com',
            respondedDate: '2024-01-19',
            response: '기여자 등록은 회원가입 후 마이페이지에서 기여자 신청을 하시면 됩니다. 심사 후 승인되면 기술자료를 등록하실 수 있습니다.'
        },
        {
            inquiryId: 'INQ-2024-0004',
            inquiryDate: '2024-01-17',
            inquirerName: '최민지',
            inquirerEmail: 'mj.choi@posco.com',
            inquirerTeam: '포스코',
            inquirerPhone: '010-4567-8901',
            title: '환불 요청',
            content: '구매한 기술자료가 예상과 달라 환불을 요청드립니다.',
            status: 'answered',
            responderName: '관리자',
            responderEmail: 'admin@rms.com',
            respondedDate: '2024-01-18',
            response: '환불 처리 완료되었습니다. 3-5 영업일 내에 결제 수단으로 환불됩니다.'
        },
        {
            inquiryId: 'INQ-2024-0005',
            inquiryDate: '2024-01-16',
            inquirerName: '정수빈',
            inquirerEmail: 'sb.jung@doosan.com',
            inquirerTeam: '두산중공업',
            inquirerPhone: '010-5678-9012',
            title: '대량 구매 할인 문의',
            content: '팀 단위로 여러 기술자료를 구매하려고 합니다. 대량 구매 할인이 가능한가요?',
            status: 'answered',
            responderName: '관리자',
            responderEmail: 'admin@rms.com',
            respondedDate: '2024-01-17',
            response: '대량 구매 시 별도 협의 가능합니다. 영업팀(sales@rms.com)으로 문의해주세요.'
        },
    ]);

    // 필터 상태
    let statusFilter = $state('all');

    // 필터링된 데이터
    let filteredInquiries = $derived(
        statusFilter === 'all'
            ? inquiries
            : inquiries.filter(i => i.status === statusFilter)
    );

    // 상태별 건수
    let pendingCount = $derived(inquiries.filter(i => i.status === 'pending').length);
    let answeredCount = $derived(inquiries.filter(i => i.status === 'answered').length);

    // 페이지네이션
    let currentPage = $state(1);
    const itemsPerPage = 10;
    let totalPages = $derived(Math.ceil(filteredInquiries.length / itemsPerPage));

    // 행 클릭 시 상세 페이지로 이동
    function handleRowClick(inquiry) {
        goto(`/rms-admin/inquiries/${inquiry.inquiryId}`);
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리', href: '/rms-admin/dashboard' },
            { label: '문의사항' }
        ]} 
    />

    <PageHeader 
        title="문의사항 관리" 
        description="사용자 문의를 확인하고 답변합니다."
        icon="MessageSquareText"
    />

    <!-- 통계 카드 -->
    <div class="grid grid-cols-3 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                    <Icon name="MessageSquare" size="md" class="text-blue-600 dark:text-blue-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{inquiries.length}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">전체 문의</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-amber-100 dark:bg-amber-900 flex items-center justify-center">
                    <Icon name="Clock" size="md" class="text-amber-600 dark:text-amber-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{pendingCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">답변 대기</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                    <Icon name="CheckCircle" size="md" class="text-green-600 dark:text-green-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{answeredCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">답변 완료</p>
                </div>
            </div>
        </div>
    </div>

    <!-- 필터 탭 + 테이블 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center justify-between">
                <div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg">
                    <button
                        onclick={() => statusFilter = 'all'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'all' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        전체
                        <span class="ml-1 text-xs text-gray-400">({inquiries.length})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'pending'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors flex items-center gap-2 {statusFilter === 'pending' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        답변 대기
                        {#if pendingCount > 0}
                            <span class="px-1.5 py-0.5 text-xs bg-amber-100 text-amber-700 dark:bg-amber-900 dark:text-amber-300 rounded-full">
                                {pendingCount}
                            </span>
                        {/if}
                    </button>
                    <button
                        onclick={() => statusFilter = 'answered'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'answered' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        답변 완료
                        <span class="ml-1 text-xs text-gray-400">({answeredCount})</span>
                    </button>
                </div>
            </div>
        </header>
        
        <section class="p-6">
            <InquiryTable 
                data={filteredInquiries}
                onRowClick={handleRowClick}
                emptyMessage={statusFilter === 'pending' ? '답변 대기 중인 문의가 없습니다.' : statusFilter === 'answered' ? '답변 완료된 문의가 없습니다.' : '문의 내역이 없습니다.'}
            />
        </section>
        
        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination 
                bind:currentPage={currentPage}
                totalPages={totalPages}
                totalItems={filteredInquiries.length}
                itemsPerPage={itemsPerPage}
            />
        </footer>
    </div>
</div>