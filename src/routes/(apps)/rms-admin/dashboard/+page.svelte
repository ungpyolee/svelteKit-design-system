<!-- /routes/(apps)/rms-admin/dashboard/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader } from "$lib/components/ui";
    import InquiryTable from "../InquiryTable.svelte";
    import DigitalAssetApprovalTable from "../DigitalAssetApprovalTable.svelte";

    // ========== 통계 데이터 ==========
    let stats = $state({
        totalAssets: 156,
        pendingApprovals: 8,
        pendingInquiries: 5,
        totalContributors: 42,
        totalSales: 1240,
        monthlyRevenue: 12500000
    });

    // ========== 승인 대기 기술자료 (최근 5건) ==========
    let pendingAssets = $state([
        {
            assetId: 'DA-2024-0001',
            title: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetType: '설계도면',
            motorType: 'IPMSM',
            price: 150000,
            registeredDate: '2024-01-20',
            contributorName: '김철수',
            contributorEmail: 'cs.kim@clew.tech',
            contributorTeam: 'CLEW Technologies',
            status: 'pending',
            approver: null,
            processedDate: null,
            processDescription: null
        },
        {
            assetId: 'DA-2024-0002',
            title: 'SPM-2024-0156 표면부착형 영구자석 전동기 사양서',
            assetType: '사양서',
            motorType: 'SPMSM',
            price: 0,
            registeredDate: '2024-01-19',
            contributorName: '이민수',
            contributorEmail: 'ms.lee@clew.tech',
            contributorTeam: 'CLEW Technologies',
            status: 'pending',
            approver: null,
            processedDate: null,
            processDescription: null
        },
        {
            assetId: 'DA-2024-0003',
            title: 'BLDC-2024-0567 BLDC 모터 제어 알고리즘 문서',
            assetType: '기술문서',
            motorType: 'BLDC',
            price: 75000,
            registeredDate: '2024-01-18',
            contributorName: '최지훈',
            contributorEmail: 'jh.choi@geneers.com',
            contributorTeam: 'Geneers',
            status: 'pending',
            approver: null,
            processedDate: null,
            processDescription: null
        },
    ]);

    // ========== 답변 대기 문의사항 (최근 5건) ==========
    let pendingInquiries = $state([
        {
            inquiryId: 'INQ-2024-0001',
            inquiryDate: '2024-01-20',
            inquirerName: '박지성',
            inquirerEmail: 'js.park@samsung.com',
            inquirerTeam: '삼성전자',
            title: '기술자료 다운로드 오류 문의',
            status: 'pending',
            responderName: null,
            responderEmail: null
        },
        {
            inquiryId: 'INQ-2024-0002',
            inquiryDate: '2024-01-19',
            inquirerName: '김영수',
            inquirerEmail: 'ys.kim@hyundai.com',
            inquirerTeam: '현대자동차',
            title: '결제 관련 문의',
            status: 'pending',
            responderName: null,
            responderEmail: null
        },
        {
            inquiryId: 'INQ-2024-0003',
            inquiryDate: '2024-01-18',
            inquirerName: '이준호',
            inquirerEmail: 'jh.lee@lg.com',
            inquirerTeam: 'LG전자',
            title: '기술자료 등록 절차 문의',
            status: 'pending',
            responderName: null,
            responderEmail: null
        },
    ]);

    // ========== 이벤트 핸들러 ==========
    function handleAssetApprove(asset, description) {
        console.log('Approved:', asset.assetId, description);
    }

    function handleAssetReject(asset, description) {
        console.log('Rejected:', asset.assetId, description);
    }

    function handleInquiryRowClick(inquiry) {
        window.location.href = `/rms-admin/inquiries/${inquiry.inquiryId}`;
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리' }
        ]} 
    />

    <PageHeader 
        title="관리자 대시보드" 
        description="RMS 시스템 현황을 한눈에 확인하세요."
        icon="LayoutDashboard"
    />

    <!-- 통계 카드 -->
    <div class="grid grid-cols-2 lg:grid-cols-3 xl:grid-cols-6 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                    <Icon name="FolderOpen" size="sm" class="text-blue-600 dark:text-blue-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{stats.totalAssets}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">전체 기술자료</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-amber-100 dark:bg-amber-900 flex items-center justify-center">
                    <Icon name="Clock" size="sm" class="text-amber-600 dark:text-amber-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{stats.pendingApprovals}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">승인 대기</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-red-100 dark:bg-red-900 flex items-center justify-center">
                    <Icon name="MessageSquare" size="sm" class="text-red-600 dark:text-red-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{stats.pendingInquiries}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">답변 대기</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-purple-100 dark:bg-purple-900 flex items-center justify-center">
                    <Icon name="Users" size="sm" class="text-purple-600 dark:text-purple-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{stats.totalContributors}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">기여자</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                    <Icon name="ShoppingCart" size="sm" class="text-green-600 dark:text-green-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{stats.totalSales}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">총 판매</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-emerald-100 dark:bg-emerald-900 flex items-center justify-center">
                    <Icon name="Banknote" size="sm" class="text-emerald-600 dark:text-emerald-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{(stats.monthlyRevenue / 10000).toLocaleString()}만</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">이번달 매출</p>
                </div>
            </div>
        </div>
    </div>

    <!-- 알림 배너 (대기 건이 있을 경우) -->
    {#if stats.pendingApprovals > 0 || stats.pendingInquiries > 0}
        <div class="p-4 bg-amber-50 dark:bg-amber-900 border border-amber-200 dark:border-amber-800 rounded-xl">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-full bg-amber-100 dark:bg-amber-800 flex items-center justify-center">
                    <Icon name="Bell" size="sm" class="text-amber-600 dark:text-amber-400" />
                </div>
                <div class="flex-1">
                    <p class="text-sm font-medium text-amber-800 dark:text-amber-200">처리가 필요한 항목이 있습니다</p>
                    <p class="text-xs text-amber-600 dark:text-amber-400 mt-0.5">
                        승인 대기 {stats.pendingApprovals}건, 답변 대기 {stats.pendingInquiries}건
                    </p>
                </div>
                <div class="flex items-center gap-2">
                    <a
                        href="/rms-admin/digital-assets?status=pending"
                        class="px-3 py-1.5 text-xs font-medium bg-amber-200 dark:bg-amber-800 text-amber-800 dark:text-amber-200 rounded-lg hover:bg-amber-300 dark:hover:bg-amber-700 transition-colors"
                    >
                        승인 처리
                    </a>
                    <a
                        href="/rms-admin/inquiries?status=pending"
                        class="px-3 py-1.5 text-xs font-medium bg-amber-200 dark:bg-amber-800 text-amber-800 dark:text-amber-200 rounded-lg hover:bg-amber-300 dark:hover:bg-amber-700 transition-colors"
                    >
                        문의 답변
                    </a>
                </div>
            </div>
        </div>
    {/if}

    <!-- 테이블 그리드 -->
    <div class="grid grid-cols-1 gap-6">
        <!-- 승인 대기 기술자료 -->
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
            <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                <div class="flex items-center gap-3">
                    <div class="w-8 h-8 rounded-lg bg-amber-100 dark:bg-amber-900 flex items-center justify-center">
                        <Icon name="FileCheck" size="sm" class="text-amber-600 dark:text-amber-400" />
                    </div>
                    <div>
                        <p class="text-base font-medium text-gray-800 dark:text-gray-100">승인 대기 기술자료</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{pendingAssets.length}건의 승인 요청</p>
                    </div>
                </div>
                <a
                    href="/rms-admin/digital-assets?status=pending"
                    class="text-sm text-primary hover:text-primary-600 font-medium flex items-center gap-1"
                >
                    전체보기
                    <Icon name="ArrowRight" size="xs" />
                </a>
            </header>
            <section class="p-4">
                <DigitalAssetApprovalTable 
                    data={pendingAssets}
                    showTeam={false}
                    onApprove={handleAssetApprove}
                    onReject={handleAssetReject}
                    emptyMessage="승인 대기 중인 기술자료가 없습니다."
                    titleMaxWidth="180px"
                />
            </section>
        </div>

        <!-- 답변 대기 문의사항 -->
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
            <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                <div class="flex items-center gap-3">
                    <div class="w-8 h-8 rounded-lg bg-red-100 dark:bg-red-900 flex items-center justify-center">
                        <Icon name="MessageSquareText" size="sm" class="text-red-600 dark:text-red-400" />
                    </div>
                    <div>
                        <p class="text-base font-medium text-gray-800 dark:text-gray-100">답변 대기 문의사항</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{pendingInquiries.length}건의 답변 요청</p>
                    </div>
                </div>
                <a
                    href="/rms-admin/inquiries?status=pending"
                    class="text-sm text-primary hover:text-primary-600 font-medium flex items-center gap-1"
                >
                    전체보기
                    <Icon name="ArrowRight" size="xs" />
                </a>
            </header>
            <section class="p-4">
                <InquiryTable 
                    data={pendingInquiries}
                    showTeam={false}
                    onRowClick={handleInquiryRowClick}
                    emptyMessage="답변 대기 중인 문의가 없습니다."
                    titleMaxWidth="180px"
                />
            </section>
        </div>
    </div>

</div>