<!-- /routes/(apps)/rms/DigitalAssetTable.svelte -->
<!-- 기술자료 목록 테이블 컴포넌트 -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";

    /**
     * @typedef {Object} DigitalAsset
     * @property {string|number} id - 자료 ID
     * @property {string} title - 자료 제목
     * @property {string} assetType - 자료 타입 (설계도면, 사양서, 기술문서 등)
     * @property {string} motorType - 전동기 타입 (IPMSM, SPMSM, BLDC 등)
     * @property {string} registrant - 등록인 이름
     * @property {string} registrantEmail - 등록인 이메일
     * @property {string} registeredDate - 등록일
     * @property {number} [price] - 가격 (선택)
     * @property {number} [salesCount] - 판매 수 (선택)
     */

    let {
        // 데이터
        data = [],
        // 컬럼 표시 옵션
        showPrice = false,
        showSalesCount = false,
        // 이벤트 핸들러
        onRowClick = () => {},
        // 빈 상태 메시지
        emptyMessage = '등록된 기술자료가 없습니다.',
        // 타이틀 최대 너비
        titleMaxWidth = '250px',
    } = $props();

    // 자료 타입별 스타일
    const assetTypeConfig = {
        '설계도면': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400' },
        '사양서': { class: 'bg-info-100 text-info-700 dark:bg-info-900/30 dark:text-info-400' },
        '시험성적서': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400' },
        '기술문서': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400' },
        '해석보고서': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        '회로도': { class: 'bg-pink-100 text-pink-700 dark:bg-pink-900/30 dark:text-pink-400' },
    };

    // 전동기 타입별 스타일
    const motorTypeConfig = {
        'IPMSM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'SPMSM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'IM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'BLDC': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
        'SRM': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
    };

    // 모달 상태
    let showDetailModal = $state(false);
    let selectedAsset = $state(null);

    function handleRowClick(asset) {
        selectedAsset = asset;
        showDetailModal = true;
        onRowClick(asset);
    }

    function formatPrice(price) {
        if (price === 0 || price === undefined) return null;
        return price.toLocaleString('ko-KR') + '원';
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">자료 타입</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">전동기 타입</th>
                {#if showPrice}
                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-right">가격</th>
                {/if}
                {#if showSalesCount}
                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-center">판매</th>
                {/if}
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록인</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-12"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as asset}
                <tr
                    class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(asset)}
                >
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                <Icon name="FileText" size="sm"/>
                            </div>
                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate" style="max-width: {titleMaxWidth}">
                                {asset.title}
                            </span>
                        </div>
                    </td>
                    <td class="px-4 py-3">
                        <span class="px-2 py-1 text-xs font-medium {assetTypeConfig[asset.assetType]?.class || 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300'} rounded-full">
                            {asset.assetType}
                        </span>
                    </td>
                    <td class="px-4 py-3">
                        <span class="px-2 py-1 text-xs font-medium {motorTypeConfig[asset.motorType]?.class || 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300'} rounded">
                            {asset.motorType}
                        </span>
                    </td>
                    {#if showPrice}
                        <td class="px-4 py-3 text-right">
                            {#if asset.price === 0 || asset.price === undefined}
                                <span class="inline-flex items-center gap-1 px-2 py-1 bg-green-100 dark:bg-green-900/30 text-green-700 dark:text-green-400 text-xs font-medium rounded-full">
                                    <Icon name="Gift" size="xs" />
                                    무료
                                </span>
                            {:else}
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">
                                    {formatPrice(asset.price)}
                                </span>
                            {/if}
                        </td>
                    {/if}
                    {#if showSalesCount}
                        <td class="px-4 py-3 text-center">
                            <span class="text-sm text-gray-600 dark:text-gray-400">{asset.salesCount ?? 0}건</span>
                        </td>
                    {/if}
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
                            onclick={(e) => { e.stopPropagation(); handleRowClick(asset); }}
                        >
                            <Icon name="ChevronRight" size="sm"/>
                        </button>
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
{:else}
    <div class="text-center py-12">
        <div class="w-16 h-16 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
            <Icon name="FileX" size="lg" />
        </div>
        <p class="text-gray-500 dark:text-gray-400">{emptyMessage}</p>
    </div>
{/if}

<!-- 기술자료 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="기술자료 상세" size="lg">
    {#if selectedAsset}
        <div class="space-y-6">
            <!-- 기술자료 정보 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="FileText" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedAsset.title}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">ID: #{selectedAsset.id}</p>
                </div>
            </div>

            <!-- 타입 정보 -->
            <div class="flex items-center gap-2">
                <span class="px-2 py-1 text-xs font-medium {assetTypeConfig[selectedAsset.assetType]?.class || 'bg-gray-100 text-gray-700'} rounded-full">
                    {selectedAsset.assetType}
                </span>
                <span class="px-2 py-1 text-xs font-medium {motorTypeConfig[selectedAsset.motorType]?.class || 'bg-gray-100 text-gray-700'} rounded">
                    {selectedAsset.motorType}
                </span>
            </div>

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 {showPrice && showSalesCount ? 'md:grid-cols-4' : showPrice || showSalesCount ? 'md:grid-cols-3' : ''} gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.registeredDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">자료 타입</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.assetType}</p>
                </div>
                {#if showPrice}
                    <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                        <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">가격</p>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">
                            {#if selectedAsset.price === 0 || selectedAsset.price === undefined}
                                무료
                            {:else}
                                {formatPrice(selectedAsset.price)}
                            {/if}
                        </p>
                    </div>
                {/if}
                {#if showSalesCount}
                    <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                        <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">판매수</p>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedAsset.salesCount ?? 0}건</p>
                    </div>
                {/if}
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
            href="/rms/digital-assets/{selectedAsset?.id}"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            상세 페이지로 이동
        </a>
    {/snippet}
</Modal>
