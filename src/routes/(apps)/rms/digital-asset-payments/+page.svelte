<!-- /routes/rms/digital-assets/sales/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, Modal, PageHeader, Pagination } from "$lib/components/ui";

    // ========== 판매내역 데이터 (더미) ==========
    let salesHistory = $state([
        {
            id: 1,
            assetId: 1,
            assetTitle: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetType: '설계도면',
            buyerName: '박지성',
            buyerEmail: 'js.park@samsung.com',
            buyerCompany: '삼성전자',
            price: 150000,
            purchasedAt: '2024-01-20 14:32',
            status: 'completed'
        },
        {
            id: 2,
            assetId: 2,
            assetTitle: 'SPM-2024-0156 표면부착형 영구자석 전동기 사양서',
            assetType: '사양서',
            buyerName: '김영수',
            buyerEmail: 'ys.kim@hyundai.com',
            buyerCompany: '현대자동차',
            price: 0,
            purchasedAt: '2024-01-19 09:15',
            status: 'completed'
        },
        {
            id: 3,
            assetId: 4,
            assetTitle: 'BLDC-2024-0567 BLDC 모터 제어 알고리즘 문서',
            assetType: '기술문서',
            buyerName: '이준호',
            buyerEmail: 'jh.lee@lg.com',
            buyerCompany: 'LG전자',
            price: 75000,
            purchasedAt: '2024-01-18 16:45',
            status: 'refunded'
        },
        {
            id: 4,
            assetId: 1,
            assetTitle: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetType: '설계도면',
            buyerName: '최민지',
            buyerEmail: 'mj.choi@posco.com',
            buyerCompany: '포스코',
            price: 150000,
            purchasedAt: '2024-01-17 11:20',
            status: 'completed'
        },
        {
            id: 5,
            assetId: 5,
            assetTitle: 'SRM-2024-0891 스위치드 릴럭턴스 전동기 해석 보고서',
            assetType: '해석보고서',
            buyerName: '정수빈',
            buyerEmail: 'sb.jung@doosan.com',
            buyerCompany: '두산중공업',
            price: 50000,
            purchasedAt: '2024-01-16 08:50',
            status: 'completed'
        },
    ]);

    // ========== 통계 데이터 ==========
    let stats = $derived({
        totalSales: salesHistory.length,
        totalRevenue: salesHistory
            .filter(s => s.status === 'completed')
            .reduce((sum, s) => sum + s.price, 0),
        freeDownloads: salesHistory.filter(s => s.price === 0 && s.status === 'completed').length,
        refunds: salesHistory.filter(s => s.status === 'refunded').length
    });

    // ========== 상태 뱃지 설정 ==========
    const statusConfig = {
        completed: { label: '완료', class: 'bg-green-100 text-green-700 dark:bg-green-900 dark:text-green-300' },
        refunded: { label: '환불', class: 'bg-red-100 text-red-700 dark:bg-red-900 dark:text-red-300' },
        pending: { label: '대기', class: 'bg-yellow-100 text-yellow-700 dark:bg-yellow-900 dark:text-yellow-300' },
    };

    // ========== 자료 타입 설정 ==========
    const assetTypeConfig = {
        '설계도면': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900 dark:text-primary-400' },
        '사양서': { class: 'bg-info-100 text-info-700 dark:bg-info-900 dark:text-info-400' },
        '시험성적서': { class: 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400' },
        '기술문서': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900 dark:text-warning-400' },
        '해석보고서': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900 dark:text-purple-400' },
    };


    // 상태 설정 가져오기 함수
    function getStatusConfig(status) {
        return statusConfig[status] || { label: '알 수 없음', class: 'bg-gray-100 text-gray-700' };
    }

    // ========== 모달 상태 ==========
    let showSaleDetailModal = $state(false);
    let selectedSale = $state(null);

    function handleSaleRowClick(sale) {
        selectedSale = sale;
        showSaleDetailModal = true;
    }

    // ========== 페이지네이션 ==========
    let currentPage = $state(1);
    const totalItems = 88;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);
</script>


<div class="space-y-6">
    <!-- 브레드크럼 -->
    <Breadcrumb
        items={[
            { label: 'RMS' },
            { label: '기술자료 판매내역' }
        ]} 
    />
    <!-- 헤더 -->
    <PageHeader 
        title="기술자료 판매내역" 
        description="판매 통계와 목록을 확인합니다."
    />
    <div class="grid grid-cols-1 gap-4">
        <!-- 통계 카드 -->
        <div class="grid grid-cols-4 gap-4">
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                        <Icon name="ShoppingCart" size="md" class="text-blue-600 dark:text-blue-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.totalSales}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">총 판매건수</p>
                    </div>
                </div>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                        <Icon name="DollarSign" size="md" class="text-green-600 dark:text-green-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.totalRevenue.toLocaleString('ko-KR')}원</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">총 판매금액</p>
                    </div>
                </div>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-purple-100 dark:bg-purple-900 flex items-center justify-center">
                        <Icon name="Handshake" size="md" class="text-purple-600 dark:text-purple-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.freeDownloads}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">무료 제공</p>
                    </div>
                </div>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                <div class="flex items-center gap-4">
                    <div class="w-12 h-12 rounded-xl bg-red-100 dark:bg-red-900 flex items-center justify-center">
                        <Icon name="RotateCcw" size="md" class="text-red-600 dark:text-red-400" />
                    </div>
                    <div>
                        <p class="text-2xl font-bold text-gray-900 dark:text-white">{stats.refunds}</p>
                        <p class="text-sm text-gray-500 dark:text-gray-400">환불</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- 판매내역 테이블 -->
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
            <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">판매내역</p>
                <div class="flex items-center gap-2">
                    <button class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 bg-gray-100 dark:bg-gray-800 rounded-lg hover:bg-gray-200 dark:hover:bg-gray-700 transition-colors flex items-center">
                        <Icon name="Download" size="sm" class="inline mr-1" />
                        내보내기
                    </button>
                </div>
            </header>
            <section class="p-6">
                <table class="w-full">
                    <thead>
                        <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">기술자료</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">구매자</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-right">결제금액</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-center">상태</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">구매일시</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                        {#each salesHistory as sale}
                            <tr 
                                class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors cursor-pointer"
                                onclick={() => handleSaleRowClick(sale)}
                            >
                                <td class="px-4 py-3">
                                    <div>
                                        <p class="text-sm text-gray-800 dark:text-gray-200 truncate max-w-[300px]">{sale.assetTitle}</p>
                                        <span class="inline-block mt-1 px-2 py-0.5 text-xs font-medium {assetTypeConfig[sale.assetType]?.class} rounded-full">
                                            {sale.assetType}
                                        </span>
                                    </div>
                                </td>
                                <td class="px-4 py-3">
                                    <div>
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{sale.buyerName}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400">{sale.buyerCompany}</p>
                                    </div>
                                </td>
                                <td class="px-4 py-3 text-right">
                                    {#if sale.price === 0}
                                        <span class="inline-flex items-center gap-1 px-2 py-1 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 text-xs font-medium rounded-full">
                                            <Icon name="Handshake" size="xs" />
                                            무료
                                        </span>
                                    {:else}
                                        <span class="text-sm font-medium text-gray-800 dark:text-gray-200">
                                            {sale.price.toLocaleString('ko-KR')}원
                                        </span>
                                    {/if}
                                </td>
                                <td class="px-4 py-3 text-center">
                                    {#if sale.status}
                                        {@const status = statusConfig[sale.status]}
                                        <span class="inline-flex px-2 py-1 text-xs font-medium rounded-full {status.class}">
                                            {status.label}
                                        </span>
                                    {/if}
                                </td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                    {sale.purchasedAt}
                                </td>
                                <td class="px-4 py-3">
                                    <button 
                                        class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                        onclick={(e) => { e.stopPropagation(); handleSaleRowClick(sale); }}
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
</div>

<!-- 판매 상세 모달 -->
<Modal bind:isOpen={showSaleDetailModal} title="판매 상세" size="lg">
    {#if selectedSale}
        <div class="space-y-6">
            <!-- 기술자료 정보 -->
            <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-2">기술자료</p>
                <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedSale.assetTitle}</p>
                <span class="inline-block mt-2 px-2 py-0.5 text-xs font-medium {assetTypeConfig[selectedSale.assetType]?.class} rounded-full">
                    {selectedSale.assetType}
                </span>
            </div>

            <!-- 거래 정보 -->
            <div class="grid grid-cols-3 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">결제금액</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">
                        {#if selectedSale.price === 0}
                            무료
                        {:else}
                            {selectedSale.price.toLocaleString('ko-KR')}원
                        {/if}
                    </p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">상태</p>
                    <span class="inline-flex px-2 py-1 text-xs font-medium rounded-full {getStatusConfig(selectedSale.status).class}">
                        {getStatusConfig(selectedSale.status).label}
                    </span>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">구매일시</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedSale.purchasedAt}</p>
                </div>
            </div>

            <!-- 구매자 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">구매자 정보</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                        {selectedSale.buyerName.charAt(0)}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedSale.buyerName}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedSale.buyerCompany} · {selectedSale.buyerEmail}</p>
                    </div>
                </div>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <button 
            onclick={() => showSaleDetailModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            닫기
        </button>
        <a 
            href="/rms/assets/{selectedSale?.assetId}"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            기술자료 보기
        </a>
    {/snippet}
</Modal>