<script>
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import DigitalAssetRequestTable from "../DigitalAssetRequestTable.svelte";

    // ========== 기술자료 요청 목록 (통일된 데이터 구조) ==========
    let digitalAssetRequests = $state([
        {
            requestId: 1,
            digitalAsset: 'IPM-2024-0892 모터 설계 도면',
            assetId: 'a1b2c3d4-5678-9012-abcd-ef1234567890',
            requestDate: '2024-01-15',
            requester: '김철수',
            requesterEmail: 'cs.kim@clew.tech',
            requesterTeam: 'Motorspace',
            requestDescription: '신규 전기차 프로젝트에 적용할 IPM 모터 설계 검토를 위해 해당 도면이 필요합니다. 특히 냉각 구조 부분을 참고하고자 합니다.',
            status: 'approved',
            approver: '박영희',
            approverEmail: 'yh.park@clew.tech',
            processedDate: '2024-01-16',
            processDescription: '프로젝트 목적 확인 완료. 냉각 구조 관련 도면 제공 승인합니다. 단, 외부 유출 금지 조건 부여.',
        },
        {
            requestId: 2,
            digitalAsset: 'BMS-2024-0156 배터리 관리 시스템 사양서',
            assetId: 'b2c3d4e5-6789-0123-bcde-f12345678901',
            requestDate: '2024-01-14',
            requester: '이민수',
            requesterEmail: 'ms.lee@clew.tech',
            requesterTeam: 'CLEW',
            requestDescription: 'BMS 시스템 통합 테스트를 위해 상세 사양서가 필요합니다. 통신 프로토콜 및 안전 기준 부분을 확인하려 합니다.',
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
            requester: '정다운',
            requesterEmail: 'dw.jung@motorspace.io',
            requesterTeam: 'Motorspace',
            requestDescription: '인버터 고장 분석을 위해 회로도 검토가 필요합니다. 전력부 및 게이트 드라이버 회로 부분을 확인하고자 합니다.',
            status: 'pending',
            approver: null,
            approverEmail: null,
            processedDate: null,
            processDescription: null,
        },
        {
            requestId: 4,
            digitalAsset: 'MCU-2024-0567 제어기 펌웨어 문서',
            assetId: 'd4e5f6g7-8901-2345-defg-234567890123',
            requestDate: '2024-01-12',
            requester: '최지훈',
            requesterEmail: 'jh.choi@geneers.com',
            requesterTeam: 'GENEERS',
            requestDescription: '펌웨어 커스터마이징을 위해 소스코드 및 문서가 필요합니다.',
            status: 'rejected',
            approver: '김대표',
            approverEmail: 'ceo@clew.tech',
            processedDate: '2024-01-13',
            processDescription: '해당 자료는 핵심 기술 자산으로 분류되어 외부 공유가 불가합니다. API 문서로 대체 제공 가능하니 별도 요청 바랍니다.',
        },
        {
            requestId: 5,
            digitalAsset: 'SEN-2024-0891 센서 인터페이스 규격서',
            assetId: 'e5f6g7h8-9012-3456-efgh-345678901234',
            requestDate: '2024-01-11',
            requester: '한소영',
            requesterEmail: 'sy.han@clew.tech',
            requesterTeam: 'CLEW',
            requestDescription: '센서 모듈 개발을 위해 인터페이스 규격 확인이 필요합니다. CAN 통신 프로토콜 부분을 중점적으로 검토하려 합니다.',
            status: 'pending',
            approver: null,
            approverEmail: null,
            processedDate: null,
            processDescription: null,
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

    // 페이지네이션
    let currentPage = $state(1);
    const totalItems = 156;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function handlePageChange(page) {
        console.log('Page changed to:', page);
        // API 호출 로직
    }
</script>

<div class="space-y-6">
    <!-- 브레드크럼 -->
    <Breadcrumb
        items={[
            { label: 'RMS' },
            { label: '기술자료 요청 목록' }
        ]}
    />
    <!-- 헤더 -->
    <PageHeader
        title="기술자료 요청 목록"
        description="무료로 제공하는 기술자료에 대한 요청 목록입니다."
    />

    <div class="grid grid-cols-1 gap-4">
        <!-- 기술자료 요청 목록 Table -->
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
            <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기술자료 요청 목록</p>
            </header>
            <section class="p-6">
                <DigitalAssetRequestTable
                    data={digitalAssetRequests}
                    showTeam={true}
                    showProcessActions={true}
                    onApprove={handleApprove}
                    onReject={handleReject}
                    titleMaxWidth="250px"
                />
            </section>
            <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
                <Pagination
                    bind:currentPage={currentPage}
                    totalPages={totalPages}
                    totalItems={totalItems}
                    itemsPerPage={itemsPerPage}
                />
            </footer>
        </div>
    </div>
</div>
