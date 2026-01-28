<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import DigitalAssetTable from "../DigitalAssetTable.svelte";

    // ========== 기술자료 목록 (통일된 데이터 구조) ==========
    let digitalAssets = $state([
        {
            id: 1,
            title: 'IPM-2024-0892 고효율 영구자석 동기전동기 설계 도면',
            assetType: '설계도면',
            motorType: 'IPMSM',
            registrant: '김철수',
            registrantEmail: 'cs.kim@clew.tech',
            registeredDate: '2024-01-15',
            price: 150000,
            salesCount: 23,
        },
        {
            id: 2,
            title: 'SPM-2024-0156 표면부착형 영구자석 전동기 사양서',
            assetType: '사양서',
            motorType: 'SPMSM',
            registrant: '이민수',
            registrantEmail: 'ms.lee@clew.tech',
            registeredDate: '2024-01-14',
            price: 0,
            salesCount: 45,
        },
        {
            id: 4,
            title: 'BLDC-2024-0567 BLDC 모터 제어 알고리즘 문서',
            assetType: '기술문서',
            motorType: 'BLDC',
            registrant: '최지훈',
            registrantEmail: 'jh.choi@geneers.com',
            registeredDate: '2024-01-12',
            price: 75000,
            salesCount: 12,
        },
        {
            id: 5,
            title: 'SRM-2024-0891 스위치드 릴럭턴스 전동기 해석 보고서',
            assetType: '해석보고서',
            motorType: 'SRM',
            registrant: '한소영',
            registrantEmail: 'sy.han@clew.tech',
            registeredDate: '2024-01-11',
            price: 50000,
            salesCount: 8,
        },
    ]);

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
            { label: '기술자료' },
        ]}
    />

    <!-- 헤더 -->
    <PageHeader
        title="기술자료 목록"
        description="등록된 기술자료 목록입니다."
    />

    <div class="grid grid-cols-1 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
            <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기술자료 목록</p>
                <div class="flex items-center gap-2">
                    <a href="digital-assets/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                        <Icon name="FilePlus" size="sm" class="inline mr-1" />
                        새 기술자료
                    </a>
                </div>
            </header>
            <section class="p-6">
                <DigitalAssetTable
                    data={digitalAssets}
                    showPrice={true}
                    showSalesCount={true}
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
