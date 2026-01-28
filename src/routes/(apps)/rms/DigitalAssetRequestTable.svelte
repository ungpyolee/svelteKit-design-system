<!-- /routes/(apps)/rms/DigitalAssetRequestTable.svelte -->
<!-- 기술자료 요청 목록 테이블 컴포넌트 -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";

    /**
     * @typedef {Object} DigitalAssetRequest
     * @property {string|number} requestId - 요청 ID
     * @property {string} digitalAsset - 기술자료 제목
     * @property {string|number} [assetId] - 기술자료 ID (선택)
     * @property {string} requestDate - 요청일
     * @property {string} requester - 요청인 이름
     * @property {string} requesterEmail - 요청인 이메일
     * @property {string} [requesterTeam] - 요청인 소속 (선택)
     * @property {string} [requestDescription] - 요청 사유 (선택)
     * @property {string} status - 상태 (approved, pending, rejected)
     * @property {string|null} approver - 승인자 이름
     * @property {string|null} [approverEmail] - 승인자 이메일 (선택)
     * @property {string|null} [processedDate] - 처리일 (선택)
     * @property {string|null} [processDescription] - 처리 사유 (선택)
     */

    let {
        // 데이터
        data = [],
        // 컬럼 표시 옵션
        showTeam = true,
        showProcessActions = true,
        // 이벤트 핸들러
        onRowClick = () => {},
        onApprove = () => {},
        onReject = () => {},
        // 빈 상태 메시지
        emptyMessage = '요청 내역이 없습니다.',
        // 타이틀 최대 너비
        titleMaxWidth = '200px',
    } = $props();

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

    // 모달 상태
    let showDetailModal = $state(false);
    let selectedRequest = $state(null);

    // 승인/반려 모달
    let showProcessModal = $state(false);
    let processType = $state(''); // 'approve' | 'reject'
    let processDescription = $state('');

    function handleRowClick(request) {
        selectedRequest = request;
        showDetailModal = true;
        onRowClick(request);
    }

    function openProcessModal(request, type, e) {
        if (e) e.stopPropagation();
        selectedRequest = request;
        processType = type;
        processDescription = '';
        showProcessModal = true;
    }

    function handleProcess() {
        if (processType === 'approve') {
            onApprove(selectedRequest, processDescription);
        } else {
            onReject(selectedRequest, processDescription);
        }
        showProcessModal = false;
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청인</th>
                {#if showTeam}
                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">소속</th>
                {/if}
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">승인상태</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">승인자</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-28"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as request}
                <tr
                    class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(request)}
                >
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                <Icon name="FileText" size="sm"/>
                            </div>
                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate" style="max-width: {titleMaxWidth}">
                                {request.digitalAsset}
                            </span>
                        </div>
                    </td>
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {request.requestDate}
                    </td>
                    <td class="px-4 py-3">
                        <div class="text-left">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{request.requester}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{request.requesterEmail}</p>
                        </div>
                    </td>
                    {#if showTeam}
                        <td class="px-4 py-3">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{request.requesterTeam || '-'}</p>
                        </td>
                    {/if}
                    <td class="px-4 py-3">
                        <span class="px-2 py-1 text-xs font-medium {statusConfig[request.status]?.class || statusConfig.pending.class} rounded-full">
                            {statusConfig[request.status]?.label || '알 수 없음'}
                        </span>
                    </td>
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {request.approver || '-'}
                    </td>
                    <td class="px-4 py-3">
                        {#if showProcessActions && request.status === 'pending'}
                            <div class="flex items-center gap-1">
                                <button
                                    onclick={(e) => openProcessModal(request, 'approve', e)}
                                    class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 hover:bg-success-200 dark:bg-success-900/30 dark:text-success-400 dark:hover:bg-success-900/50 rounded transition-colors"
                                >
                                    승인
                                </button>
                                <button
                                    onclick={(e) => openProcessModal(request, 'reject', e)}
                                    class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 hover:bg-danger-200 dark:bg-danger-900/30 dark:text-danger-400 dark:hover:bg-danger-900/50 rounded transition-colors"
                                >
                                    반려
                                </button>
                            </div>
                        {:else}
                            <button
                                class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                onclick={(e) => { e.stopPropagation(); handleRowClick(request); }}
                            >
                                <Icon name="ChevronRight" size="sm"/>
                            </button>
                        {/if}
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
{:else}
    <div class="text-center py-12">
        <div class="w-16 h-16 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
            <Icon name="Inbox" size="lg" />
        </div>
        <p class="text-gray-500 dark:text-gray-400">{emptyMessage}</p>
    </div>
{/if}

<!-- 요청 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="기술자료 요청 상세" size="lg">
    {#if selectedRequest}
        <div class="space-y-6">
            <!-- 기술자료 정보 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="FileText" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedRequest.digitalAsset}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">요청 ID: #{selectedRequest.requestId}</p>
                </div>
                <span class="px-2 py-1 text-xs font-medium {statusConfig[selectedRequest.status]?.class} rounded-full">
                    {statusConfig[selectedRequest.status]?.label}
                </span>
            </div>

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">요청일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRequest.requestDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">승인자</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRequest.approver || '-'}</p>
                </div>
            </div>

            <!-- 요청인 정보 -->
            <div class="border border-gray-100 dark:border-gray-800 rounded-lg">
                <div class="p-4">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">요청인 정보</p>
                    <div class="flex items-center gap-3">
                        <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                            {selectedRequest.requester?.charAt(0) || '?'}
                        </div>
                        <div>
                            <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRequest.requester}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">
                                {selectedRequest.requesterEmail}
                                {#if selectedRequest.requesterTeam}
                                    · {selectedRequest.requesterTeam}
                                {/if}
                            </p>
                        </div>
                    </div>
                </div>
                {#if selectedRequest.requestDescription}
                    <div class="p-4 border-t border-gray-100 dark:border-gray-800">
                        <p class="text-xs text-gray-500 dark:text-gray-400 mb-2">요청 내용</p>
                        <p class="text-sm text-gray-700 dark:text-gray-300">
                            {selectedRequest.requestDescription}
                        </p>
                    </div>
                {/if}
            </div>

            <!-- 처리 정보 (승인/반려된 경우) -->
            {#if selectedRequest.status !== 'pending' && selectedRequest.processDescription}
                <div class="p-4 border-l-4 {selectedRequest.status === 'approved' ? 'border-success bg-success-50 dark:bg-success-900/10' : 'border-danger bg-danger-50 dark:bg-danger-900/10'} rounded-r-lg">
                    <div class="flex items-center justify-between mb-2">
                        <p class="text-xs font-medium {selectedRequest.status === 'approved' ? 'text-success-700 dark:text-success-400' : 'text-danger-700 dark:text-danger-400'}">
                            {selectedRequest.status === 'approved' ? '승인' : '반려'} 사유
                        </p>
                        {#if selectedRequest.processedDate}
                            <p class="text-xs text-gray-500 dark:text-gray-400">{selectedRequest.processedDate}</p>
                        {/if}
                    </div>
                    <p class="text-sm text-gray-700 dark:text-gray-300 mb-3">{selectedRequest.processDescription}</p>
                    {#if selectedRequest.approver}
                        <div class="flex items-center gap-2 text-xs text-gray-500 dark:text-gray-400">
                            <span>처리자:</span>
                            <span class="font-medium text-gray-700 dark:text-gray-300">{selectedRequest.approver}</span>
                        </div>
                    {/if}
                </div>
            {/if}

            <!-- 승인/반려 입력 (대기 중인 경우) -->
            {#if showProcessActions && selectedRequest.status === 'pending'}
                <div class="border border-gray-100 dark:border-gray-800 rounded-lg p-4">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">승인/반려 내용</p>
                    <textarea
                        class="w-full px-3 py-2 text-sm bg-gray-50 dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg resize-none focus:outline-none focus:ring-2 focus:ring-primary"
                        rows="3"
                        placeholder="승인/반려 사유를 입력해주세요."
                    ></textarea>
                </div>
            {/if}
        </div>
    {/if}

    {#snippet footer()}
        <button
            onclick={() => showDetailModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            닫기
        </button>
        {#if showProcessActions && selectedRequest?.status === 'pending'}
            <button
                onclick={() => { showDetailModal = false; openProcessModal(selectedRequest, 'reject'); }}
                class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
            >
                반려
            </button>
            <button
                onclick={() => { showDetailModal = false; openProcessModal(selectedRequest, 'approve'); }}
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                승인
            </button>
        {:else if selectedRequest?.assetId}
            <a
                href="/rms/digital-assets/{selectedRequest.assetId}#requests"
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                상세 페이지로 이동
            </a>
        {/if}
    {/snippet}
</Modal>

<!-- 승인/반려 처리 모달 -->
<Modal bind:isOpen={showProcessModal} title={processType === 'approve' ? '요청 승인' : '요청 반려'} size="md">
    {#if selectedRequest}
        <div class="space-y-4">
            <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                <div class="flex items-center gap-3 mb-3">
                    <div class="w-8 h-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 text-sm font-medium">
                        {selectedRequest.requester?.charAt(0) || '?'}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRequest.requester}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedRequest.requesterTeam || selectedRequest.requesterEmail}</p>
                    </div>
                </div>
                <p class="text-sm text-gray-600 dark:text-gray-400">{selectedRequest.requestDescription || selectedRequest.digitalAsset}</p>
            </div>

            <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    {processType === 'approve' ? '승인' : '반려'} 사유 <span class="text-danger">*</span>
                </label>
                <textarea
                    bind:value={processDescription}
                    rows="4"
                    placeholder={processType === 'approve' ? '승인 사유를 입력하세요...' : '반려 사유를 입력하세요...'}
                    class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                ></textarea>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <button
            onclick={() => showProcessModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button
            onclick={handleProcess}
            disabled={!processDescription.trim()}
            class="px-4 py-2 text-sm font-medium text-white rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed {processType === 'approve' ? 'bg-success hover:bg-success-600' : 'bg-danger hover:bg-danger-600'}"
        >
            {processType === 'approve' ? '승인' : '반려'}
        </button>
    {/snippet}
</Modal>
