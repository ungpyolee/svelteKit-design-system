<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader } from "$lib/components/ui";
    import DigitalAssetTable from "../DigitalAssetTable.svelte";
    import DigitalAssetRequestTable from "../DigitalAssetRequestTable.svelte";

    // ========== 통계 데이터 ==========
    let stats = $derived({
        totalAssets: 156,
        pendingRequests: 12,
        totalSales: 3,
        freeProvided: 4
    });

    // ========== 기술자료 요청 목록 (통일된 데이터 구조) ==========
    let digitalAssetRequests = $state([
        {
            requestId: 1,
            digitalAsset: 'IPM-2024-0892 모터 설계 도면',
            assetId: 'a1b2c3d4-5678-9012-abcd-ef1234567890',
            requestDate: '2024-01-15',
            requester: '정다운',
            requesterEmail: 'dw.jung@motorspace.io',
            requesterTeam: 'Motorspace',
            requestDescription: '신규 전기차 프로젝트에 적용할 IPM 모터 설계 검토를 위해 해당 도면이 필요합니다.',
            status: 'approved',
            approver: '박영희',
            approverEmail: 'yh.park@clew.tech',
            processedDate: '2024-01-16',
            processDescription: '프로젝트 목적 확인 완료. 외부 유출 금지 조건 부여.',
        },
        {
            requestId: 2,
            digitalAsset: 'BMS-2024-0156 배터리 관리 시스템 사양서',
            assetId: 'b2c3d4e5-6789-0123-bcde-f12345678901',
            requestDate: '2024-01-14',
            requester: '이민수',
            requesterEmail: 'ms.lee@clew.tech',
            requesterTeam: 'CLEW',
            requestDescription: 'BMS 시스템 통합 테스트를 위해 상세 사양서가 필요합니다.',
            status: 'approved',
            approver: '박영희',
            approverEmail: 'yh.park@clew.tech',
            processedDate: '2024-01-15',
            processDescription: '내부 프로젝트 용도로 승인합니다.',
        },
        {
            requestId: 3,
            digitalAsset: 'INV-2024-0234 인버터 회로도',
            assetId: 'c3d4e5f6-7890-1234-cdef-123456789012',
            requestDate: '2024-01-13',
            requester: '김태호',
            requesterEmail: 'th.kim@geneers.com',
            requesterTeam: 'GENEERS',
            requestDescription: '인버터 고장 분석을 위해 회로도 검토가 필요합니다.',
            status: 'pending',
            approver: null,
            approverEmail: null,
            processedDate: null,
            processDescription: null,
        },
    ]);

    // ========== 최근 등록된 기술자료 (통일된 데이터 구조) ==========
    let recentAssets = $state([
        {
            id: 1,
            title: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetType: '설계도면',
            motorType: 'IPMSM',
            registrant: '김철수',
            registrantEmail: 'cs.kim@clew.tech',
            registeredDate: '2024-01-15',
        },
        {
            id: 2,
            title: 'SPM-2024-0156 표면부착형 영구자석 전동기 사양서',
            assetType: '사양서',
            motorType: 'SPMSM',
            registrant: '이민수',
            registrantEmail: 'ms.lee@clew.tech',
            registeredDate: '2024-01-14',
        },
        {
            id: 4,
            title: 'BLDC-2024-0567 BLDC 모터 제어 알고리즘 문서',
            assetType: '기술문서',
            motorType: 'BLDC',
            registrant: '최지훈',
            registrantEmail: 'jh.choi@geneers.com',
            registeredDate: '2024-01-12',
        },
    ]);

    // 요청 승인/반려 핸들러
    function handleApprove(request, description) {
        console.log('Approve:', request.requestId, description);
        // API 호출 로직
    }

    function handleReject(request, description) {
        console.log('Reject:', request.requestId, description);
        // API 호출 로직
    }
</script>

<div class="space-y-6">
    <!-- 브레드크럼 -->
    <Breadcrumb
        items={[
            { label: 'RMS' },
            { label: '기여자 대시보드' }
        ]}
    />

    <!-- 헤더 -->
    <PageHeader
        title="기여자 대시보드"
        description="전체 현황과 최근 변동사항을 확인합니다."
    />

    <div class="grid grid-cols-1 gap-4">
        <!-- 통계 카드 -->
        <div class="grid grid-cols-4 gap-4">
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                        <Icon name="FolderOpen" size="md" class="text-blue-600 dark:text-blue-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.totalAssets}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">기술자료 보유</p>
                    </div>
                </div>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-amber-100 dark:bg-amber-900 flex items-center justify-center">
                        <Icon name="ClipboardList" size="md" class="text-amber-600 dark:text-amber-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.pendingRequests}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">요청 대기</p>
                    </div>
                </div>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                        <Icon name="ShoppingCart" size="md" class="text-green-600 dark:text-green-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.totalSales}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">총 판매건수</p>
                    </div>
                </div>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-purple-100 dark:bg-purple-900 flex items-center justify-center">
                        <Icon name="Handshake" size="md" class="text-purple-600 dark:text-purple-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.freeProvided}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">무료 제공</p>
                    </div>
                </div>
            </div>
        </div>

        <div class="grid grid-cols-1 gap-4">
            <!-- 기술자료 요청 목록 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기술자료 요청 목록</p>
                    <a href="/rms/digital-asset-requests" class="text-sm text-primary hover:text-primary-700">
                        전체보기
                    </a>
                </header>
                <section class="p-6">
                    <DigitalAssetRequestTable
                        data={digitalAssetRequests}
                        showTeam={true}
                        showProcessActions={true}
                        onApprove={handleApprove}
                        onReject={handleReject}
                    />
                </section>
            </div>

            <!-- 최근 등록된 기술자료 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">최근 등록된 기술자료</p>
                    <a href="/rms/digital-assets" class="text-sm text-primary hover:text-primary-700">
                        전체보기
                    </a>
                </header>
                <section class="p-6">
                    <DigitalAssetTable
                        data={recentAssets}
                        showPrice={false}
                        showSalesCount={false}
                    />
                </section>
            </div>
        </div>
    </div>
</div>
