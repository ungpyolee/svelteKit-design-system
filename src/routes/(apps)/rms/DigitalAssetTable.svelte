<!-- /routes/(apps)/rms/digital-assets/DigitalAssetTable.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";

    let {
        data = [],
        onToggleStatus = () => {},
        onRowClick = () => {},
        emptyMessage = '등록된 기술자료가 없습니다.',
        titleMaxWidth = '250px',
    } = $props();

    // 상태별 설정
    const statusConfig = {
        pending: {
            label: '승인대기',
            class: 'bg-amber-100 text-amber-700 dark:bg-amber-900 dark:text-amber-400',
            icon: 'Clock'
        },
        rejected: {
            label: '반려',
            class: 'bg-red-100 text-red-700 dark:bg-red-900 dark:text-red-400',
            icon: 'XCircle'
        },
        published: {
            label: '등록',
            class: 'bg-green-100 text-green-700 dark:bg-green-900 dark:text-green-400',
            icon: 'Eye'
        },
        unpublished: {
            label: '미등록',
            class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400',
            icon: 'EyeOff'
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

    // 전동기 타입별 스타일
    const motorTypeConfig = {
        'IPMSM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'SPMSM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'IM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'BLDC': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'SRM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
    };

    function getStatusConfig(status) {
        return statusConfig[status] || statusConfig.pending;
    }

    function getAssetTypeConfig(assetType) {
        return assetTypeConfig[assetType] || { class: 'bg-gray-100 text-gray-700' };
    }

    function getMotorTypeConfig(motorType) {
        return motorTypeConfig[motorType] || { class: 'bg-gray-100 text-gray-700' };
    }

    function formatPrice(price) {
        if (price === 0) return '무료';
        return price.toLocaleString('ko-KR') + '원';
    }

    // 모달 상태
    let showDetailModal = $state(false);
    let selectedAsset = $state(null);

    function handleRowClick(asset) {
        selectedAsset = asset;
        showDetailModal = true;
        onRowClick(asset);
    }

    function handleToggleStatus(asset, e) {
        e.stopPropagation();
        onToggleStatus(asset);
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">타입</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록인</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-right">가격 / 판매</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-center">상태</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as asset}
                <tr
                    class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(asset)}
                >
                    <!-- 기술자료 제목 -->
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                <Icon name="FileText" size="sm"/>
                            </div>
                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate" style="max-width: {titleMaxWidth}">
                                {asset.title}
                            </span>
                        </div>
                    </td>

                    <!-- 타입 (자료 + 전동기) -->
                    <td class="px-4 py-3">
                        <div class="flex flex-col gap-1">
                            <span class="inline-flex w-fit px-2 py-0.5 text-xs font-medium {getAssetTypeConfig(asset.assetType).class} rounded-full">
                                {asset.assetType}
                            </span>
                            <span class="inline-flex w-fit px-2 py-0.5 text-xs font-medium {getMotorTypeConfig(asset.motorType).class} rounded">
                                {asset.motorType}
                            </span>
                        </div>
                    </td>

                    <!-- 등록인 -->
                    <td class="px-4 py-3">
                        <div>
                            <p class="text-sm text-gray-800 dark:text-gray-200">{asset.registrant}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{asset.registrantEmail}</p>
                        </div>
                    </td>

                    <!-- 가격 / 판매 -->
                    <td class="px-4 py-3 text-right">
                        <div>
                            {#if asset.price === 0}
                                <span class="inline-flex items-center gap-1 px-2 py-0.5 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 text-xs font-medium rounded-full">
                                    <Icon name="Unlock" size="xs" />
                                    무료
                                </span>
                            {:else}
                                <p class="text-sm font-medium text-gray-800 dark:text-gray-200">
                                    {asset.price.toLocaleString('ko-KR')}원
                                </p>
                            {/if}
                            <p class="text-xs text-gray-500 dark:text-gray-400 mt-0.5">
                                판매 {asset.salesCount}건
                            </p>
                        </div>
                    </td>

                    <!-- 등록일 -->
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {asset.registeredDate}
                    </td>

                    <!-- 상태 -->
                    <td class="px-4 py-3 text-center">
                        {#if asset.status === 'published' || asset.status === 'unpublished'}
                            <button
                                onclick={(e) => handleToggleStatus(asset, e)}
                                class="inline-flex items-center gap-1 px-2 py-1 text-xs font-medium rounded-full transition-colors {getStatusConfig(asset.status).class} hover:opacity-80"
                            >
                                <Icon name={getStatusConfig(asset.status).icon} size="xs" />
                                {getStatusConfig(asset.status).label}
                            </button>
                        {:else}
                            <span class="inline-flex items-center gap-1 px-2 py-1 text-xs font-medium rounded-full {getStatusConfig(asset.status).class}">
                                <Icon name={getStatusConfig(asset.status).icon} size="xs" />
                                {getStatusConfig(asset.status).label}
                            </span>
                        {/if}
                    </td>

                    <!-- 액션 -->
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-1 justify-end">
                            <a
                                href="/rms/digital-assets/{asset.id}/edit"
                                class="p-1.5 text-gray-400 hover:text-blue-600 dark:hover:text-blue-400 hover:bg-blue-50 dark:hover:bg-blue-900 rounded-lg transition-colors"
                                onclick={(e) => e.stopPropagation()}
                                title="수정"
                            >
                                <Icon name="Pencil" size="sm"/>
                            </a>
                            <button
                                class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                onclick={(e) => { e.stopPropagation(); handleRowClick(asset); }}
                                title="상세보기"
                            >
                                <Icon name="ChevronRight" size="sm"/>
                            </button>
                        </div>
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

<!-- 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="기술자료 상세" size="lg">
    {#if selectedAsset}
        <div class="space-y-6">
            <!-- 기술자료 정보 헤더 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="FileText" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedAsset.title}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">ID: #{selectedAsset.id}</p>
                </div>
                <span class="inline-flex items-center gap-1 px-2 py-1 text-xs font-medium rounded-full {getStatusConfig(selectedAsset.status).class}">
                    <Icon name={getStatusConfig(selectedAsset.status).icon} size="xs" />
                    {getStatusConfig(selectedAsset.status).label}
                </span>
            </div>

            <!-- 타입 정보 -->
            <div class="flex items-center gap-2">
                <span class="px-2 py-1 text-xs font-medium {getAssetTypeConfig(selectedAsset.assetType).class} rounded-full">
                    {selectedAsset.assetType}
                </span>
                <span class="px-2 py-1 text-xs font-medium {getMotorTypeConfig(selectedAsset.motorType).class} rounded">
                    {selectedAsset.motorType}
                </span>
            </div>

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 md:grid-cols-4 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.registeredDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">가격</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{formatPrice(selectedAsset.price)}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">판매수</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.salesCount}건</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">상태</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{getStatusConfig(selectedAsset.status).label}</p>
                </div>
            </div>

            <!-- 등록인 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">등록인 정보</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                        {selectedAsset.registrant?.charAt(0) || '?'}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.registrant}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedAsset.registrantEmail}</p>
                    </div>
                </div>
            </div>

            <!-- 반려 사유 (반려 상태일 경우) -->
            {#if selectedAsset.status === 'rejected'}
                <div class="p-4 border-l-4 border-red-500 bg-red-50 dark:bg-red-900 rounded-r-lg">
                    <p class="text-xs font-medium text-red-700 dark:text-red-400 mb-1">반려 사유</p>
                    <p class="text-sm text-gray-700 dark:text-gray-300">저작권 확인이 필요합니다. 출처를 명시해주세요.</p>
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
        <a
            href="/rms/digital-assets/{selectedAsset?.id}/edit"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            수정하기
        </a>
    {/snippet}
</Modal>