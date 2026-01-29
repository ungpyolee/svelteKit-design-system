<!-- /routes/(apps)/rms-admin/DigitalAssetApprovalTable.svelte -->
<!-- 기술자료 등록 승인 테이블 컴포넌트 (관리자용) -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";

    /**
     * @typedef {Object} DigitalAssetApproval
     * @property {string|number} assetId - 기술자료 ID
     * @property {string} title - 기술자료 제목
     * @property {string} assetType - 자료 타입
     * @property {string} motorType - 전동기 타입
     * @property {number} price - 가격
     * @property {string} registeredDate - 등록일
     * @property {string} contributorName - 기여자 이름
     * @property {string} contributorEmail - 기여자 이메일
     * @property {string} [contributorTeam] - 기여자 소속 (선택)
     * @property {string} [description] - 기술자료 설명 (선택)
     * @property {string} status - 상태 (pending, approved, rejected)
     * @property {string|null} approver - 승인자 이름
     * @property {string|null} [approverEmail] - 승인자 이메일 (선택)
     * @property {string|null} [processedDate] - 처리일 (선택)
     * @property {string|null} [processDescription] - 처리 사유 (선택)
     */

    let {
        data = [],
        showTeam = true,
        showProcessActions = true,
        onRowClick = () => {},
        onApprove = () => {},
        onReject = () => {},
        emptyMessage = '등록된 기술자료가 없습니다.',
        titleMaxWidth = '250px',
    } = $props();

    // 상태별 스타일
    const statusConfig = {
        approved: {
            label: '승인완료',
            class: 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400'
        },
        pending: {
            label: '승인대기',
            class: 'bg-warning-100 text-warning-700 dark:bg-warning-900 dark:text-warning-400'
        },
        rejected: {
            label: '반려',
            class: 'bg-danger-100 text-danger-700 dark:bg-danger-900 dark:text-danger-400'
        }
    };

    // 자료 타입별 스타일
    const assetTypeConfig = {
        '설계도면': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900 dark:text-primary-400' },
        '사양서': { class: 'bg-info-100 text-info-700 dark:bg-info-900 dark:text-info-400' },
        '시험성적서': { class: 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400' },
        '기술문서': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900 dark:text-warning-400' },
        '해석보고서': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900 dark:text-purple-400' },
    };

    function getStatusConfig(status) {
        return statusConfig[status] || statusConfig.pending;
    }

    function getAssetTypeConfig(assetType) {
        return assetTypeConfig[assetType] || { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' };
    }

    // 모달 상태
    let showDetailModal = $state(false);
    let selectedAsset = $state(null);

    // 승인/반려 모달
    let showProcessModal = $state(false);
    let processType = $state('');
    let processDescription = $state('');

    function handleRowClick(asset) {
        selectedAsset = asset;
        showDetailModal = true;
        onRowClick(asset);
    }

    function openProcessModal(asset, type, e) {
        if (e) e.stopPropagation();
        selectedAsset = asset;
        processType = type;
        processDescription = '';
        showProcessModal = true;
    }

    function handleProcess() {
        if (processType === 'approve') {
            onApprove(selectedAsset, processDescription);
        } else {
            onReject(selectedAsset, processDescription);
        }
        showProcessModal = false;
    }

    function formatPrice(price) {
        if (price === 0) return '무료';
        return price.toLocaleString('ko-KR') + '원';
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">자료 타입</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-right">가격</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기여자</th>
                {#if showTeam}
                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">소속</th>
                {/if}
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-28"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as asset}
                <tr
                    class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(asset)}
                >
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                <Icon name="FileText" size="sm"/>
                            </div>
                            <div>
                                <span class="text-sm text-gray-800 dark:text-gray-200 truncate block" style="max-width: {titleMaxWidth}">
                                    {asset.title}
                                </span>
                                <span class="text-xs text-gray-500 dark:text-gray-400">ID: {asset.assetId}</span>
                            </div>
                        </div>
                    </td>
                    <td class="px-4 py-3">
                        <span class="px-2 py-1 text-xs font-medium {getAssetTypeConfig(asset.assetType).class} rounded-full">
                            {asset.assetType}
                        </span>
                    </td>
                    <td class="px-4 py-3 text-right">
                        {#if asset.price === 0}
                            <span class="inline-flex items-center gap-1 px-2 py-1 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 text-xs font-medium rounded-full">
                                <Icon name="Unlock" size="xs" />
                                무료
                            </span>
                        {:else}
                            <span class="text-sm font-medium text-gray-800 dark:text-gray-200">
                                {asset.price.toLocaleString('ko-KR')}원
                            </span>
                        {/if}
                    </td>
                    <td class="px-4 py-3">
                        <div class="text-left">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{asset.contributorName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{asset.contributorEmail}</p>
                        </div>
                    </td>
                    {#if showTeam}
                        <td class="px-4 py-3">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{asset.contributorTeam || '-'}</p>
                        </td>
                    {/if}
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {asset.registeredDate}
                    </td>
                    <td class="px-4 py-3">
                        <span class="px-2 py-1 text-xs font-medium {getStatusConfig(asset.status).class} rounded-full">
                            {getStatusConfig(asset.status).label}
                        </span>
                    </td>
                    <td class="px-4 py-3">
                        {#if showProcessActions && asset.status === 'pending'}
                            <div class="flex items-center gap-1">
                                <button
                                    onclick={(e) => openProcessModal(asset, 'approve', e)}
                                    class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 hover:bg-success-200 dark:bg-success-900 dark:text-success-400 dark:hover:bg-success-800 rounded transition-colors"
                                >
                                    승인
                                </button>
                                <button
                                    onclick={(e) => openProcessModal(asset, 'reject', e)}
                                    class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 hover:bg-danger-200 dark:bg-danger-900 dark:text-danger-400 dark:hover:bg-danger-800 rounded transition-colors"
                                >
                                    반려
                                </button>
                            </div>
                        {:else}
                            <button
                                class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                onclick={(e) => { e.stopPropagation(); handleRowClick(asset); }}
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
            <Icon name="FolderOpen" size="lg" />
        </div>
        <p class="text-gray-500 dark:text-gray-400">{emptyMessage}</p>
    </div>
{/if}

<!-- 기술자료 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="기술자료 등록 상세" size="lg">
    {#if selectedAsset}
        <div class="space-y-6">
            <!-- 기술자료 정보 헤더 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="FileText" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedAsset.title}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">ID: #{selectedAsset.assetId}</p>
                </div>
                <span class="px-2 py-1 text-xs font-medium {getStatusConfig(selectedAsset.status).class} rounded-full">
                    {getStatusConfig(selectedAsset.status).label}
                </span>
            </div>

            <!-- 타입 및 가격 정보 -->
            <div class="flex items-center gap-2">
                <span class="px-2 py-1 text-xs font-medium {getAssetTypeConfig(selectedAsset.assetType).class} rounded-full">
                    {selectedAsset.assetType}
                </span>
                <span class="px-2 py-1 text-xs font-medium bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300 rounded">
                    {selectedAsset.motorType}
                </span>
                {#if selectedAsset.price === 0}
                    <span class="inline-flex items-center gap-1 px-2 py-1 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 text-xs font-medium rounded-full">
                        <Icon name="Unlock" size="xs" />
                        무료
                    </span>
                {:else}
                    <span class="px-2 py-1 text-xs font-medium bg-blue-100 text-blue-700 dark:bg-blue-900 dark:text-blue-300 rounded-full">
                        {selectedAsset.price.toLocaleString('ko-KR')}원
                    </span>
                {/if}
            </div>

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.registeredDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">처리일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.processedDate || '-'}</p>
                </div>
            </div>

            <!-- 기여자 정보 -->
            <div class="border border-gray-100 dark:border-gray-800 rounded-lg">
                <div class="p-4">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">기여자 정보</p>
                    <div class="flex items-center gap-3">
                        <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                            {selectedAsset.contributorName?.charAt(0) || '?'}
                        </div>
                        <div>
                            <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.contributorName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">
                                {selectedAsset.contributorEmail}
                                {#if selectedAsset.contributorTeam}
                                    · {selectedAsset.contributorTeam}
                                {/if}
                            </p>
                        </div>
                    </div>
                </div>
                {#if selectedAsset.description}
                    <div class="p-4 border-t border-gray-100 dark:border-gray-800">
                        <p class="text-xs text-gray-500 dark:text-gray-400 mb-2">기술자료 설명</p>
                        <p class="text-sm text-gray-700 dark:text-gray-300">
                            {selectedAsset.description}
                        </p>
                    </div>
                {/if}
            </div>

            <!-- 처리 정보 (승인/반려된 경우) -->
            {#if selectedAsset.status !== 'pending' && (selectedAsset.processDescription || selectedAsset.approver)}
                <div class="p-4 border-l-4 {selectedAsset.status === 'approved' ? 'border-success bg-success-50 dark:bg-success-900' : 'border-danger bg-danger-50 dark:bg-danger-900'} rounded-r-lg">
                    <div class="flex items-center justify-between mb-2">
                        <p class="text-xs font-medium {selectedAsset.status === 'approved' ? 'text-success-700 dark:text-success-400' : 'text-danger-700 dark:text-danger-400'}">
                            {selectedAsset.status === 'approved' ? '승인' : '반려'} 내역
                        </p>
                        {#if selectedAsset.processedDate}
                            <p class="text-xs text-gray-500 dark:text-gray-400">{selectedAsset.processedDate}</p>
                        {/if}
                    </div>
                    {#if selectedAsset.processDescription}
                        <p class="text-sm text-gray-700 dark:text-gray-300 mb-3">{selectedAsset.processDescription}</p>
                    {/if}
                    {#if selectedAsset.approver}
                        <div class="flex items-center gap-2 text-xs text-gray-500 dark:text-gray-400">
                            <span>처리자:</span>
                            <span class="font-medium text-gray-700 dark:text-gray-300">
                                {selectedAsset.approver}
                                {#if selectedAsset.approverEmail}
                                    ({selectedAsset.approverEmail})
                                {/if}
                            </span>
                        </div>
                    {/if}
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
        {#if showProcessActions && selectedAsset?.status === 'pending'}
            <button
                onclick={() => { showDetailModal = false; openProcessModal(selectedAsset, 'reject'); }}
                class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
            >
                반려
            </button>
            <button
                onclick={() => { showDetailModal = false; openProcessModal(selectedAsset, 'approve'); }}
                class="px-4 py-2 text-sm font-medium bg-success text-white rounded-lg hover:bg-success-600 transition-colors"
            >
                승인
            </button>
        {:else}
            <a
                href="/rms-admin/digital-assets/{selectedAsset?.assetId}"
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
            >
                상세 페이지로 이동
            </a>
        {/if}
    {/snippet}
</Modal>

<!-- 승인/반려 처리 모달 -->
<Modal bind:isOpen={showProcessModal} title={processType === 'approve' ? '기술자료 승인' : '기술자료 반려'} size="md">
    {#if selectedAsset}
        <div class="space-y-4">
            <!-- 기술자료 요약 -->
            <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                <div class="flex items-center gap-3 mb-3">
                    <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400">
                        <Icon name="FileText" size="sm"/>
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.title}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">
                            {selectedAsset.contributorName} · {selectedAsset.contributorTeam || selectedAsset.contributorEmail}
                        </p>
                    </div>
                </div>
                <div class="flex items-center gap-2">
                    <span class="px-2 py-1 text-xs font-medium {getAssetTypeConfig(selectedAsset.assetType).class} rounded-full">
                        {selectedAsset.assetType}
                    </span>
                    <span class="text-xs text-gray-500 dark:text-gray-400">
                        {formatPrice(selectedAsset.price)}
                    </span>
                </div>
            </div>

            <!-- 저작권 확인 체크 (승인 시) -->
            {#if processType === 'approve'}
                <div class="p-4 border border-amber-200 dark:border-amber-800 bg-amber-50 dark:bg-amber-900 rounded-lg">
                    <div class="flex items-start gap-3">
                        <Icon name="AlertTriangle" size="sm" class="text-amber-600 dark:text-amber-400 mt-0.5" />
                        <div>
                            <p class="text-sm font-medium text-amber-800 dark:text-amber-200">저작권 확인</p>
                            <p class="text-xs text-amber-700 dark:text-amber-300 mt-1">
                                해당 기술자료가 저작권 및 지적재산권에 문제가 없음을 확인하셨나요?
                            </p>
                        </div>
                    </div>
                </div>
            {/if}

            <!-- 사유 입력 -->
            <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    {processType === 'approve' ? '승인' : '반려'} 사유
                    {#if processType === 'reject'}
                        <span class="text-danger">*</span>
                    {/if}
                </label>
                <textarea
                    bind:value={processDescription}
                    rows="4"
                    placeholder={processType === 'approve' ? '승인 사유를 입력하세요 (선택)' : '반려 사유를 입력하세요 (필수)'}
                    class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                ></textarea>
                {#if processType === 'reject'}
                    <p class="text-xs text-gray-500 dark:text-gray-400 mt-1">
                        반려 사유는 기여자에게 전달됩니다.
                    </p>
                {/if}
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
            disabled={processType === 'reject' && !processDescription.trim()}
            class="px-4 py-2 text-sm font-medium text-white rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed {processType === 'approve' ? 'bg-success hover:bg-success-600' : 'bg-danger hover:bg-danger-600'}"
        >
            {processType === 'approve' ? '승인' : '반려'}
        </button>
    {/snippet}
</Modal>