<!-- /routes/(apps)/rms-admin/digital-assets/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import DigitalAssetApprovalTable from "../DigitalAssetApprovalTable.svelte";

    // 더미 데이터
    let digitalAssets = $state([
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
            description: '고효율 영구자석 동기전동기의 상세 설계 도면입니다. CAD 파일 포함.',
            status: 'pending',
            approver: null,
            approverEmail: null,
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
            description: '표면부착형 영구자석 전동기의 기술 사양서입니다.',
            status: 'pending',
            approver: null,
            approverEmail: null,
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
            description: 'BLDC 모터의 FOC 제어 알고리즘 상세 문서입니다.',
            status: 'approved',
            approver: '관리자',
            approverEmail: 'admin@rms.com',
            processedDate: '2024-01-19',
            processDescription: '저작권 확인 완료. 판매 승인합니다.'
        },
        {
            assetId: 'DA-2024-0004',
            title: 'SRM-2024-0891 스위치드 릴럭턴스 전동기 해석 보고서',
            assetType: '해석보고서',
            motorType: 'SRM',
            price: 50000,
            registeredDate: '2024-01-17',
            contributorName: '한소영',
            contributorEmail: 'sy.han@clew.tech',
            contributorTeam: 'CLEW Technologies',
            description: 'SRM 전동기의 전자기 해석 결과 보고서입니다.',
            status: 'rejected',
            approver: '관리자',
            approverEmail: 'admin@rms.com',
            processedDate: '2024-01-18',
            processDescription: '참고 문헌 출처가 불명확합니다. 저작권 확인이 필요합니다.'
        },
    ]);

    // 필터
    let statusFilter = $state('all');

    let filteredAssets = $derived(
        statusFilter === 'all' 
            ? digitalAssets 
            : digitalAssets.filter(a => a.status === statusFilter)
    );

    let pendingCount = $derived(digitalAssets.filter(a => a.status === 'pending').length);

    // 페이지네이션
    let currentPage = $state(1);
    const itemsPerPage = 10;
    let totalPages = $derived(Math.ceil(filteredAssets.length / itemsPerPage));

    // 이벤트 핸들러
    function handleApprove(asset, description) {
        console.log('Approved:', asset.assetId, description);
        // API 호출
    }

    function handleReject(asset, description) {
        console.log('Rejected:', asset.assetId, description);
        // API 호출
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리', href: '/rms-admin/dashboard' },
            { label: '기술자료 관리' }
        ]} 
    />

    <PageHeader 
        title="기술자료 관리" 
        description="기여자가 등록한 기술자료를 검토하고 승인/반려합니다."
        icon="FolderCheck"
    />

    <!-- 필터 탭 -->
    <div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg w-fit">
        <button
            onclick={() => statusFilter = 'all'}
            class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'all' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
        >
            전체
        </button>
        <button
            onclick={() => statusFilter = 'pending'}
            class="px-4 py-2 text-sm font-medium rounded-md transition-colors flex items-center gap-2 {statusFilter === 'pending' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
        >
            승인 대기
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
        </button>
        <button
            onclick={() => statusFilter = 'rejected'}
            class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'rejected' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
        >
            반려
        </button>
    </div>

    <!-- 테이블 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <section class="p-6">
            <DigitalAssetApprovalTable 
                data={filteredAssets}
                onApprove={handleApprove}
                onReject={handleReject}
            />
        </section>
        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination 
                bind:currentPage={currentPage}
                totalPages={totalPages}
                totalItems={filteredAssets.length}
                itemsPerPage={itemsPerPage}
            />
        </footer>
    </div>
</div>