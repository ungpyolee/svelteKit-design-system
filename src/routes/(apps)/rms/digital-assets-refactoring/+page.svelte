<!-- /routes/(apps)/rms/digital-assets/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import StatCard from "$lib/components/ui/stats/StatCard.svelte";
    import SearchInput from "$lib/components/ui/filter/SearchInput.svelte";
    import FilterTabs from "$lib/components/ui/filter/FilterTabs.svelte";
    import FilterToggleGroup from "$lib/components/ui/filter/FilterToggleGroup.svelte";
    import PriceRangeFilter from "$lib/components/ui/filter/PriceRangeFilter.svelte";
    import AppliedFilters from "$lib/components/ui/filter/AppliedFilters.svelte";
    import SortBar from "$lib/components/ui/sort/SortBar.svelte";
    import DigitalAssetTable from "../DigitalAssetTable.svelte";
    
    import { 
        assetTypeOptions, 
        motorTypeOptions, 
        sortOptions, 
        statusOptions,
        statusConfig 
    } from './_config.js';

    // ========== 더미 데이터 (나중에 API로 대체) ==========
    // ========== 기술자료 목록 ==========
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
            status: 'published'
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
            status: 'published'
        },
        {
            id: 3,
            title: 'IM-2024-0234 유도전동기 효율 시험성적서',
            assetType: '시험성적서',
            motorType: 'IM',
            registrant: '박영희',
            registrantEmail: 'yh.park@clew.tech',
            registeredDate: '2024-01-13',
            price: 80000,
            salesCount: 0,
            status: 'pending'
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
            status: 'unpublished'
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
            status: 'published'
        },
        {
            id: 6,
            title: 'IPMSM-2024-0123 고속 전동기 설계 도면',
            assetType: '설계도면',
            motorType: 'IPMSM',
            registrant: '김철수',
            registrantEmail: 'cs.kim@clew.tech',
            registeredDate: '2024-01-10',
            price: 200000,
            salesCount: 0,
            status: 'rejected'
        },
        {
            id: 7,
            title: 'BLDC-2024-0789 BLDC 모터 사양서',
            assetType: '사양서',
            motorType: 'BLDC',
            registrant: '이민수',
            registrantEmail: 'ms.lee@clew.tech',
            registeredDate: '2024-01-09',
            price: 0,
            salesCount: 0,
            status: 'pending'
        },
        {
            id: 8,
            title: 'IM-2024-0456 유도전동기 설계 초안',
            assetType: '설계도면',
            motorType: 'IM',
            registrant: '박영희',
            registrantEmail: 'yh.park@clew.tech',
            registeredDate: '2024-01-08',
            price: 0,
            salesCount: 0,
            status: 'draft'
        },
        {
            id: 9,
            title: 'SRM-2024-0999 SRM 해석 보고서 (작성중)',
            assetType: '해석보고서',
            motorType: 'SRM',
            registrant: '한소영',
            registrantEmail: 'sy.han@clew.tech',
            registeredDate: '2024-01-07',
            price: 0,
            salesCount: 0,
            status: 'draft'
        },
    ]);

    // ========== 필터 상태 ==========
    let statusFilter = $state('all');
    let selectedAssetTypes = $state([]);
    let selectedMotorTypes = $state([]);
    let searchQuery = $state('');
    let priceMin = $state('');
    let priceMax = $state('');
    let showFilters = $state(false);
    let sortBy = $state('');

    // ========== 상태별 건수 (탭 옵션에 count 추가) ==========
    let statusTabOptions = $derived(
        statusOptions.map(opt => ({
            ...opt,
            count: opt.value === 'all' 
                ? digitalAssets.length 
                : digitalAssets.filter(a => a.status === opt.value).length
        }))
    );

    // ========== 통계 카드 데이터 ==========
    let statCards = $derived([
        { value: digitalAssets.length, label: '전체', icon: 'FolderOpen', color: 'blue' },
        { value: digitalAssets.filter(a => a.status === 'draft').length, label: '임시저장', icon: 'FileEdit', color: 'slate' },
        { value: digitalAssets.filter(a => a.status === 'pending').length, label: '승인대기', icon: 'Clock', color: 'amber' },
        { value: digitalAssets.filter(a => a.status === 'published').length, label: '등록', icon: 'Eye', color: 'green' },
        { value: digitalAssets.filter(a => a.status === 'unpublished').length, label: '미등록', icon: 'EyeOff', color: 'gray' },
        { value: digitalAssets.filter(a => a.status === 'rejected').length, label: '반려', icon: 'XCircle', color: 'red' },
    ]);

    // ========== 필터링 및 정렬 ==========
    let filteredAssets = $derived(() => {
        let result = digitalAssets;

        if (statusFilter !== 'all') {
            result = result.filter(a => a.status === statusFilter);
        }
        if (selectedAssetTypes.length > 0) {
            result = result.filter(a => selectedAssetTypes.includes(a.assetType));
        }
        if (selectedMotorTypes.length > 0) {
            result = result.filter(a => selectedMotorTypes.includes(a.motorType));
        }
        if (searchQuery.trim()) {
            const query = searchQuery.toLowerCase();
            result = result.filter(a => 
                a.title.toLowerCase().includes(query) ||
                a.registrant.toLowerCase().includes(query) ||
                a.registrantEmail.toLowerCase().includes(query)
            );
        }
        if (priceMin !== '') {
            result = result.filter(a => a.price >= Number(priceMin));
        }
        if (priceMax !== '') {
            result = result.filter(a => a.price <= Number(priceMax));
        }

        if (sortBy) {
            result = [...result].sort((a, b) => {
                switch (sortBy) {
                    case 'salesDesc': return b.salesCount - a.salesCount;
                    case 'salesAsc': return a.salesCount - b.salesCount;
                    case 'priceDesc': return b.price - a.price;
                    case 'priceAsc': return a.price - b.price;
                    case 'dateDesc': return new Date(b.registeredDate) - new Date(a.registeredDate);
                    case 'dateAsc': return new Date(a.registeredDate) - new Date(b.registeredDate);
                    default: return 0;
                }
            });
        }

        return result;
    });

    // ========== 적용 필터 태그 ==========
    let appliedFilters = $derived(() => {
        const filters = [];
        
        if (searchQuery) {
            filters.push({
                key: 'search',
                label: `검색: ${searchQuery}`,
                color: 'gray',
                onRemove: () => searchQuery = ''
            });
        }
        
        selectedAssetTypes.forEach(type => {
            filters.push({
                key: `asset-${type}`,
                label: type,
                color: 'blue',
                onRemove: () => selectedAssetTypes = selectedAssetTypes.filter(t => t !== type)
            });
        });
        
        selectedMotorTypes.forEach(type => {
            filters.push({
                key: `motor-${type}`,
                label: type,
                color: 'purple',
                onRemove: () => selectedMotorTypes = selectedMotorTypes.filter(t => t !== type)
            });
        });
        
        if (priceMin !== '' || priceMax !== '') {
            filters.push({
                key: 'price',
                label: `가격: ${priceMin || '0'}원 ~ ${priceMax || '∞'}원`,
                color: 'green',
                onRemove: () => { priceMin = ''; priceMax = ''; }
            });
        }
        
        return filters;
    });

    // ========== 활성 필터 개수 ==========
    let activeFilterCount = $derived(() => {
        let count = 0;
        if (selectedAssetTypes.length > 0) count++;
        if (selectedMotorTypes.length > 0) count++;
        if (priceMin !== '' || priceMax !== '') count++;
        return count;
    });

    // ========== 페이지네이션 ==========
    let currentPage = $state(1);
    const itemsPerPage = 10;
    let totalPages = $derived(Math.ceil(filteredAssets().length / itemsPerPage));

    // ========== 이벤트 핸들러 ==========
    function handleToggleStatus(asset) {
        const index = digitalAssets.findIndex(a => a.id === asset.id);
        if (index !== -1) {
            if (digitalAssets[index].status === 'published') {
                digitalAssets[index].status = 'unpublished';
            } else if (digitalAssets[index].status === 'unpublished') {
                digitalAssets[index].status = 'published';
            }
        }
    }

    function handleDelete(asset) {
        digitalAssets = digitalAssets.filter(a => a.id !== asset.id);
    }

    function clearFilters() {
        selectedAssetTypes = [];
        selectedMotorTypes = [];
        priceMin = '';
        priceMax = '';
        searchQuery = '';
        sortBy = '';
    }
</script>

<div class="space-y-6">
    <Breadcrumb items={[{ label: 'RMS', href: '/rms/user-dashboard' }, { label: '기술자료' }]} />

    <PageHeader title="기술자료 목록" description="등록된 기술자료를 관리합니다." icon="FolderOpen">
        <a href="/rms/digital-assets/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-2">
            <Icon name="FilePlus" size="sm" />
            새 기술자료
        </a>
    </PageHeader>

    <!-- 통계 카드 -->
    <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-4">
        {#each statCards as card}
            <StatCard {...card} />
        {/each}
    </div>

    <!-- 테이블 카드 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 space-y-4">
            <div class="flex flex-col lg:flex-row lg:items-center justify-between gap-4">
                <FilterTabs options={statusTabOptions} bind:value={statusFilter} />

                <div class="flex items-center gap-2">
                    <SearchInput bind:value={searchQuery} placeholder="제목, 등록인 검색..." />
                    
                    <button
                        onclick={() => showFilters = !showFilters}
                        class="px-4 py-2 text-sm font-medium border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors flex items-center gap-2 {showFilters ? 'bg-gray-50 dark:bg-gray-800' : ''}"
                    >
                        <Icon name="SlidersHorizontal" size="sm" />
                        필터
                        {#if activeFilterCount() > 0}
                            <span class="px-1.5 py-0.5 text-xs bg-primary text-white rounded-full">
                                {activeFilterCount()}
                            </span>
                        {/if}
                    </button>
                </div>
            </div>

            {#if showFilters}
                <div class="pt-4 border-t border-gray-100 dark:border-gray-800 space-y-4">
                    <FilterToggleGroup 
                        label="자료 타입" 
                        options={assetTypeOptions} 
                        bind:selected={selectedAssetTypes} 
                        color="blue" 
                    />
                    
                    <FilterToggleGroup 
                        label="전동기 타입" 
                        options={motorTypeOptions} 
                        bind:selected={selectedMotorTypes} 
                        color="purple" 
                    />

                    <div class="flex flex-wrap items-end gap-4">
                        <PriceRangeFilter bind:min={priceMin} bind:max={priceMax} />
                        
                        <button
                            onclick={clearFilters}
                            class="px-4 py-2 text-sm font-medium text-gray-600 dark:text-gray-400 border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors flex items-center gap-2"
                        >
                            <Icon name="RotateCcw" size="sm" />
                            필터 초기화
                        </button>
                    </div>

                    <AppliedFilters filters={appliedFilters()} />
                </div>
            {/if}
        </header>

        <SortBar options={sortOptions} bind:value={sortBy} />

        <section class="p-6">
            <DigitalAssetTable
                data={filteredAssets()}
                onToggleStatus={handleToggleStatus}
                onDelete={handleDelete}
                emptyMessage="조건에 맞는 기술자료가 없습니다."
            />
        </section>

        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <div class="flex items-center justify-between">
                <p class="text-sm text-gray-500 dark:text-gray-400">
                    총 {filteredAssets().length}건
                    {#if sortBy}
                        <span class="text-primary ml-2">
                            ({sortOptions.find(o => o.value === sortBy)?.label})
                        </span>
                    {/if}
                </p>
                <Pagination
                    bind:currentPage={currentPage}
                    totalPages={totalPages}
                    totalItems={filteredAssets().length}
                    itemsPerPage={itemsPerPage}
                />
            </div>
        </footer>
    </div>
</div>