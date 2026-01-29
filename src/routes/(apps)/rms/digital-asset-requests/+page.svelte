<!-- /routes/(apps)/rms/digital-asset-requests/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import DigitalAssetRequestTable from "../DigitalAssetRequestTable.svelte";

    // 더미 데이터
    let requests = $state([
        {
            requestId: 'REQ-2024-0001',
            digitalAsset: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetId: 'DA-2024-0001',
            requestDate: '2024-01-20',
            requester: '박지성',
            requesterEmail: 'js.park@samsung.com',
            requesterTeam: '삼성전자',
            requestDescription: '신규 프로젝트 참고용으로 요청드립니다.',
            status: 'pending',
            approver: null,
            approverEmail: null,
            processedDate: null,
            processDescription: null
        },
        {
            requestId: 'REQ-2024-0002',
            digitalAsset: 'SPM-2024-0156 표면부착형 영구자석 전동기 사양서',
            assetId: 'DA-2024-0002',
            requestDate: '2024-01-19',
            requester: '김영수',
            requesterEmail: 'ys.kim@hyundai.com',
            requesterTeam: '현대자동차',
            requestDescription: '기술 검토 목적으로 요청합니다.',
            status: 'pending',
            approver: null,
            approverEmail: null,
            processedDate: null,
            processDescription: null
        },
        {
            requestId: 'REQ-2024-0003',
            digitalAsset: 'BLDC-2024-0567 BLDC 모터 제어 알고리즘 문서',
            assetId: 'DA-2024-0003',
            requestDate: '2024-01-18',
            requester: '이준호',
            requesterEmail: 'jh.lee@lg.com',
            requesterTeam: 'LG전자',
            requestDescription: '내부 교육 자료로 활용하고자 합니다.',
            status: 'approved',
            approver: '김철수',
            approverEmail: 'cs.kim@clew.tech',
            processedDate: '2024-01-19',
            processDescription: '교육 목적 확인. 승인합니다.'
        },
        {
            requestId: 'REQ-2024-0004',
            digitalAsset: 'SRM-2024-0891 스위치드 릴럭턴스 전동기 해석 보고서',
            assetId: 'DA-2024-0004',
            requestDate: '2024-01-17',
            requester: '최민지',
            requesterEmail: 'mj.choi@posco.com',
            requesterTeam: '포스코',
            requestDescription: '경쟁사 분석 목적입니다.',
            status: 'rejected',
            approver: '김철수',
            approverEmail: 'cs.kim@clew.tech',
            processedDate: '2024-01-18',
            processDescription: '경쟁사 분석 목적의 요청은 승인이 어렵습니다.'
        },
        {
            requestId: 'REQ-2024-0005',
            digitalAsset: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetId: 'DA-2024-0001',
            requestDate: '2024-01-16',
            requester: '정수빈',
            requesterEmail: 'sb.jung@doosan.com',
            requesterTeam: '두산중공업',
            requestDescription: '기술 협력 검토용으로 요청드립니다.',
            status: 'approved',
            approver: '김철수',
            approverEmail: 'cs.kim@clew.tech',
            processedDate: '2024-01-17',
            processDescription: '협력사 요청 확인. 승인합니다.'
        },
    ]);

    // 필터 상태
    let statusFilter = $state('all');

    // 필터링된 데이터
    let filteredRequests = $derived(
        statusFilter === 'all'
            ? requests
            : requests.filter(r => r.status === statusFilter)
    );

    // 상태별 건수
    let pendingCount = $derived(requests.filter(r => r.status === 'pending').length);
    let approvedCount = $derived(requests.filter(r => r.status === 'approved').length);
    let rejectedCount = $derived(requests.filter(r => r.status === 'rejected').length);

    // 페이지네이션
    let currentPage = $state(1);
    const itemsPerPage = 10;
    let totalPages = $derived(Math.ceil(filteredRequests.length / itemsPerPage));

    // 이벤트 핸들러
    function handleApprove(request, description) {
        console.log('Approved:', request.requestId, description);
        // API 호출
    }

    function handleReject(request, description) {
        console.log('Rejected:', request.requestId, description);
        // API 호출
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS', href: '/rms/user-dashboard' },
            { label: '기술자료 요청' }
        ]} 
    />

    <PageHeader 
        title="기술자료 요청 관리" 
        description="내 무료 기술자료에 대한 접근 요청을 관리합니다."
        icon="ClipboardList"
    />

    <!-- 통계 카드 -->
    <div class="grid grid-cols-4 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                    <Icon name="Inbox" size="md" class="text-blue-600 dark:text-blue-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{requests.length}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">전체 요청</p>
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
                    <p class="text-sm text-gray-500 dark:text-gray-400">대기 중</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                    <Icon name="CheckCircle" size="md" class="text-green-600 dark:text-green-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{approvedCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">승인 완료</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-red-100 dark:bg-red-900 flex items-center justify-center">
                    <Icon name="XCircle" size="md" class="text-red-600 dark:text-red-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{rejectedCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">반려</p>
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
                        <span class="ml-1 text-xs text-gray-400">({requests.length})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'pending'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors flex items-center gap-2 {statusFilter === 'pending' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        대기 중
                        {#if pendingCount > 0}
                            <span class="px-1.5 py-0.5 text-xs bg-amber-100 text-amber-700 dark:bg-amber-900 dark:text-amber-300 rounded-full">
                                {pendingCount}
                            </span>
                        {/if}
                    </button>
                    <button
                        onclick={() => statusFilter = 'approved'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'approved' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        승인 완료
                        <span class="ml-1 text-xs text-gray-400">({approvedCount})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'rejected'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'rejected' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        반려
                        <span class="ml-1 text-xs text-gray-400">({rejectedCount})</span>
                    </button>
                </div>
            </div>
        </header>
        
        <section class="p-6">
            <DigitalAssetRequestTable 
                data={filteredRequests}
                onApprove={handleApprove}
                onReject={handleReject}
                emptyMessage={statusFilter === 'pending' ? '대기 중인 요청이 없습니다.' : statusFilter === 'approved' ? '승인된 요청이 없습니다.' : statusFilter === 'rejected' ? '반려된 요청이 없습니다.' : '요청 내역이 없습니다.'}
            />
        </section>
        
        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination 
                bind:currentPage={currentPage}
                totalPages={totalPages}
                totalItems={filteredRequests.length}
                itemsPerPage={itemsPerPage}
            />
        </footer>
    </div>
</div>