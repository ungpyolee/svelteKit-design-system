<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";

    let digitalAssetRequests = $state([
        { 
            requestId: 1, 
            digitalAsset: 'IPM-2024-0892 모터 설계 도면', 
            requestDate: '2024-01-15',
            requester: '김철수', 
            requesterEmail: 'cs.kim@clew.tech',
            approver: '박영희', 
            status: 'approved' 
        },
        { 
            requestId: 3, 
            digitalAsset: 'INV-2024-0234 인버터 회로도', 
            requestDate: '2024-01-13',
            requester: '정다운', 
            requesterEmail: 'dw.jung@motorspace.io',
            approver: '-', 
            status: 'pending' 
        },
        { 
            requestId: 4, 
            digitalAsset: 'MCU-2024-0567 제어기 펌웨어 문서', 
            requestDate: '2024-01-12',
            requester: '최지훈', 
            requesterEmail: 'jh.choi@geneers.com',
            approver: '박대표', 
            status: 'rejected' 
        },
    ]);

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


    // 페이지 네이션    
    let currentPage = $state(1);
    const totalItems = 156;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);
    
    function handlePageChange(page) {
        console.log('Page changed to:', page);
        // API 호출 등
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <!-- 최근 등록된 기술자료 Table -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기술자료 요청 목록</p>
        </header>
        <section class="p-6">
            <table class="w-full">
                 <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청인</th>
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
                href="/rms/requests/{selectedAsset?.requestId}"
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                상세 페이지로 이동
            </a>
        {/if}
    {/snippet}
</Modal>
