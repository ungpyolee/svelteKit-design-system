<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";
    import { assetRequests } from "$lib/config/assets";
  import { Breadcrumb, PageHeader } from "$lib/components/ui";



    // ========== 통계 데이터 ==========
    let stats = $derived({
        totalAssets: 156,  // 기술자료 보유 수
        pendingRequests: 12,  // 요청 대기 중
        totalSales: 3,
        freeProvided: 4
    });

    let digitalAssetRequests = $state(null);

    digitalAssetRequests = assetRequests? assetRequests : null;

    // 모달 상태
    let showDetailModal = $state(false);
    let selectedAsset = $state(null);

    
    // 상태별 스타일
    const statusConfig = {
        approved: {
            label: '승인완료',
            class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400'
        },
        pending: {
            label: '대기중',
            class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400'
        },
        rejected: {
            label: '반려',
            class: 'bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400'
        }
    };

    // 행 클릭 핸들러
    function handleRowClick(asset) {
        selectedAsset = asset;
        showDetailModal = true;
    }

    // 페이지 이동 (필요시 사용)
    function goToDetailPage(requestId) {
        // goto(`/rms/requests/${requestId}`);
    }


    // recentAssets 
    let recentAssets = $state([
        { 
            id: 1, 
            registeredDate: '2024-01-15',
            title: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면', 
            assetType: '설계도면',
            motorType: 'IPMSM',
            registrant: '김철수',
            registrantEmail: 'cs.kim@clew.tech'
        },
        { 
            id: 2, 
            registeredDate: '2024-01-14',
            title: 'SPM-2024-0156 표면부착형 영구자석 전동기 사양서', 
            assetType: '사양서',
            motorType: 'SPMSM',
            registrant: '이민수',
            registrantEmail: 'ms.lee@clew.tech'
        },
        { 
            id: 4, 
            registeredDate: '2024-01-12',
            title: 'BLDC-2024-0567 BLDC 모터 제어 알고리즘 문서', 
            assetType: '기술문서',
            motorType: 'BLDC',
            registrant: '최지훈',
            registrantEmail: 'jh.choi@geneers.com'
        },
        // { 
        //     id: 5, 
        //     registeredDate: '2024-01-11',
        //     title: 'SRM-2024-0891 스위치드 릴럭턴스 전동기 해석 보고서', 
        //     assetType: '해석보고서',
        //     motorType: 'SRM',
        //     registrant: '한소영',
        //     registrantEmail: 'sy.han@clew.tech'
        // },
    ]);

    // 자료 타입별 스타일
    const assetTypeConfig = {
        '설계도면': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400' },
        '사양서': { class: 'bg-info-100 text-info-700 dark:bg-info-900/30 dark:text-info-400' },
        '시험성적서': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400' },
        '기술문서': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400' },
        '해석보고서': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
    };

    // 전동기 타입별 스타일
    const motorTypeConfig = {
        'IPMSM': { label: 'IPMSM', class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'SPMSM': { label: 'SPMSM', class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'IM': { label: 'IM', class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'BLDC': { label: 'BLDC', class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'SRM': { label: 'SRM', class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
    };

    // 모달 상태 추가
    let showAssetDetailModal = $state(false);
    let selectedRecentAsset = $state(null);

    function handleAssetRowClick(asset) {
        selectedRecentAsset = asset;
        showAssetDetailModal = true;
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
            <!-- 최근 기술자료 승인 기록 Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기술자료 요청 목록</p>
                    <a href="/rms/requests" class="text-sm text-primary hover:text-primary-700">
                        전체보기
                    </a>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청일</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청인</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">소속</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">승인상태</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">승인자</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            {#each digitalAssetRequests as asset}
                                <tr 
                                    class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                                    onclick={() => handleRowClick(asset)}
                                >
                                    <td class="px-4 py-3">
                                        <div class="flex items-center gap-3">
                                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                                                <Icon name="FileText" size="sm"/>
                                            </div>
                                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate max-w-[200px]">
                                                {asset.digitalAsset}
                                            </span>
                                        </div>
                                    </td>
                                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">{asset.requestDate}</td>
                                    <td class="px-4 py-3">
                                        <div class="text-left">
                                            <p class="text-sm text-gray-800 dark:text-gray-200">{asset.requester}</p>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">{asset.requesterEmail}</p>
                                        </div>
                                    </td>
                                    <td class="px-4 py-3">
                                        <div class="text-left">
                                            <p class="text-sm text-gray-800 dark:text-gray-200">{asset.affiliation}</p>
                                        </div>
                                    </td>
                                    <td class="px-4 py-3">
                                        <span class="px-2 py-1 text-xs font-medium {statusConfig[asset.status].class} rounded-full">
                                            {statusConfig[asset.status].label}
                                        </span>
                                    </td>
                                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">{asset.approver}</td>
                                    <td class="px-4 py-3">
                                        <button 
                                            class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                            onclick={(e) => { e.stopPropagation(); handleRowClick(asset); }}
                                        >
                                            <Icon name="ChevronRight" size="sm"/>
                                        </button>
                                    </td>
                                </tr>
                            {/each}
                        </tbody>
                    </table>
                </section>
            </div>


            <!-- 최근 등록된 기술자료 Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">최근 등록된 기술자료</p>
                    <a href="/rms/assets" class="text-sm text-primary hover:text-primary-700">
                        전체보기
                    </a>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">자료 타입</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">전동기 타입</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록인</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            {#each recentAssets as asset}
                                <tr 
                                    class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                                    onclick={() => handleAssetRowClick(asset)}
                                >
                                    <td class="px-4 py-3">
                                        <div class="flex items-center gap-3">
                                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                                                <Icon name="FileText" size="sm"/>
                                            </div>
                                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate max-w-[250px]">
                                                {asset.title}
                                            </span>
                                        </div>
                                    </td>
                                    <td class="px-4 py-3">
                                        <span class="px-2 py-1 text-xs font-medium {assetTypeConfig[asset.assetType]?.class || 'bg-gray-100 text-gray-700'} rounded-full">
                                            {asset.assetType}
                                        </span>
                                    </td>
                                    <td class="px-4 py-3">
                                        <span class="px-2 py-1 text-xs font-medium {motorTypeConfig[asset.motorType]?.class} rounded">
                                            {asset.motorType}
                                        </span>
                                    </td>
                                    <td class="px-4 py-3">
                                        <div class="text-left">
                                            <p class="text-sm text-gray-800 dark:text-gray-200">{asset.registrant}</p>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">{asset.registrantEmail}</p>
                                        </div>
                                    </td>
                                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                        {asset.registeredDate}
                                    </td>
                                    <td class="px-4 py-3">
                                        <button 
                                            class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                            onclick={(e) => { e.stopPropagation(); handleAssetRowClick(asset); }}
                                        >
                                            <Icon name="ChevronRight" size="sm"/>
                                        </button>
                                    </td>
                                </tr>
                            {/each}
                        </tbody>
                    </table>
                </section>
            </div>
        </div>
    </div>
</div>

<!-- 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="기술자료 요청 상세" size="lg">
    {#if selectedAsset}
        <div class="space-y-6">
            <!-- 기술자료 정보 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="FileText" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedAsset.digitalAsset}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">요청 ID: #{selectedAsset.requestId}</p>
                </div>
                <span class="px-2 py-1 text-xs font-medium {statusConfig[selectedAsset.status].class} rounded-full">
                    {statusConfig[selectedAsset.status].label}
                </span>
            </div>

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">요청일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.requestDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">승인자</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.approver}</p>
                </div>
            </div>

            <!-- 요청인 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">요청인 정보</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                        {selectedAsset.requester.charAt(0)}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.requester}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedAsset.requesterEmail}</p>
                    </div>
                </div>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <button 
            onclick={() => showDetailModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            닫기
        </button>
        {#if selectedAsset?.status === 'pending'}
            <button 
                class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
            >
                반려
            </button>
            <button 
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                승인
            </button>
        {:else}
            <a 
                href="/rms/digital-assets/{selectedAsset?.assetId}#requests"
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                상세 페이지로 이동
            </a>
        {/if}
    {/snippet}
</Modal>


<!-- 기술자료 상세 모달 -->
<Modal bind:isOpen={showAssetDetailModal} title="기술자료 상세" size="lg">
    {#if selectedRecentAsset}
        <div class="space-y-6">
            <!-- 기술자료 정보 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="FileText" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedRecentAsset.title}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">ID: #{selectedRecentAsset.id}</p>
                </div>
            </div>

            <!-- 타입 정보 -->
            <div class="flex items-center gap-2">
                <span class="px-2 py-1 text-xs font-medium {assetTypeConfig[selectedRecentAsset.assetType]?.class} rounded-full">
                    {selectedRecentAsset.assetType}
                </span>
                <span class="px-2 py-1 text-xs font-medium {motorTypeConfig[selectedRecentAsset.motorType]?.class} rounded">
                    {selectedRecentAsset.motorType}
                </span>
            </div>

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRecentAsset.registeredDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">자료 타입</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRecentAsset.assetType}</p>
                </div>
            </div>

            <!-- 등록인 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">등록인 정보</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                        {selectedRecentAsset.registrant.charAt(0)}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRecentAsset.registrant}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedRecentAsset.registrantEmail}</p>
                    </div>
                </div>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <button 
            onclick={() => showAssetDetailModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            닫기
        </button>
        <a 
            href="/rms/assets/{selectedRecentAsset?.id}"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            상세 페이지로 이동
        </a>
    {/snippet}
</Modal>