<!-- /routes/(apps)/rms/digital-assets/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import DigitalAssetTable from "../DigitalAssetTable.svelte";

    // ========== 필터 옵션 ==========
    const assetTypeOptions = ['설계도면', '사양서', '시험성적서', '기술문서', '해석보고서'];
    const motorTypeOptions = ['IPMSM', 'SPMSM', 'IM', 'BLDC', 'SRM'];

    // ========== 정렬 옵션 ==========
    const sortOptions = [
        { value: 'salesDesc', label: '판매 많은 순', icon: 'TrendingUp' },
        { value: 'salesAsc', label: '판매 적은 순', icon: 'TrendingDown' },
        { value: 'priceDesc', label: '가격 높은 순', icon: 'ArrowUp' },
        { value: 'priceAsc', label: '가격 낮은 순', icon: 'ArrowDown' },
        { value: 'dateDesc', label: '최신 등록순', icon: 'Calendar' },
        { value: 'dateAsc', label: '오래된 순', icon: 'CalendarDays' },
    ];

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
    let selectedAssetTypes = $state([]);  // 복수 선택
    let selectedMotorTypes = $state([]);  // 복수 선택
    let searchQuery = $state('');
    let priceMin = $state('');
    let priceMax = $state('');
    let showFilters = $state(false);
    let sortBy = $state('');  // 정렬 옵션

    // ========== 자료타입 토글 ==========
    function toggleAssetType(type) {
        if (selectedAssetTypes.includes(type)) {
            selectedAssetTypes = selectedAssetTypes.filter(t => t !== type);
        } else {
            selectedAssetTypes = [...selectedAssetTypes, type];
        }
    }

    // ========== 전동기타입 토글 ==========
    function toggleMotorType(type) {
        if (selectedMotorTypes.includes(type)) {
            selectedMotorTypes = selectedMotorTypes.filter(t => t !== type);
        } else {
            selectedMotorTypes = [...selectedMotorTypes, type];
        }
    }

    // ========== 정렬 토글 ==========
    function toggleSort(value) {
        if (sortBy === value) {
            sortBy = '';
        } else {
            sortBy = value;
        }
    }

    // ========== 필터링된 데이터 ==========
    let filteredAssets = $derived(() => {
        let result = digitalAssets;

        // 상태 필터
        if (statusFilter !== 'all') {
            result = result.filter(a => a.status === statusFilter);
        }

        // 자료 타입 필터 (복수 선택)
        if (selectedAssetTypes.length > 0) {
            result = result.filter(a => selectedAssetTypes.includes(a.assetType));
        }

        // 전동기 타입 필터 (복수 선택)
        if (selectedMotorTypes.length > 0) {
            result = result.filter(a => selectedMotorTypes.includes(a.motorType));
        }

        // 검색어 필터 (제목, 등록인, 등록인 이메일)
        if (searchQuery.trim()) {
            const query = searchQuery.toLowerCase();
            result = result.filter(a => 
                a.title.toLowerCase().includes(query) ||
                a.registrant.toLowerCase().includes(query) ||
                a.registrantEmail.toLowerCase().includes(query)
            );
        }

        // 가격 범위 필터
        if (priceMin !== '') {
            result = result.filter(a => a.price >= Number(priceMin));
        }
        if (priceMax !== '') {
            result = result.filter(a => a.price <= Number(priceMax));
        }

        // 정렬
        if (sortBy) {
            result = [...result].sort((a, b) => {
                switch (sortBy) {
                    case 'salesDesc':
                        return b.salesCount - a.salesCount;
                    case 'salesAsc':
                        return a.salesCount - b.salesCount;
                    case 'priceDesc':
                        return b.price - a.price;
                    case 'priceAsc':
                        return a.price - b.price;
                    case 'dateDesc':
                        return new Date(b.registeredDate) - new Date(a.registeredDate);
                    case 'dateAsc':
                        return new Date(a.registeredDate) - new Date(b.registeredDate);
                    default:
                        return 0;
                }
            });
        }

        return result;
    });

    // ========== 상태별 건수 ==========
    let statusCounts = $derived({
        all: digitalAssets.length,
        draft: digitalAssets.filter(a => a.status === 'draft').length,
        pending: digitalAssets.filter(a => a.status === 'pending').length,
        rejected: digitalAssets.filter(a => a.status === 'rejected').length,
        published: digitalAssets.filter(a => a.status === 'published').length,
        unpublished: digitalAssets.filter(a => a.status === 'unpublished').length,
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
        console.log('Toggled status:', asset.id, digitalAssets[index].status);
    }

    function handleDelete(asset) {
        digitalAssets = digitalAssets.filter(a => a.id !== asset.id);
        console.log('Deleted:', asset.id);
    }

    function clearFilters() {
        selectedAssetTypes = [];
        selectedMotorTypes = [];
        priceMin = '';
        priceMax = '';
        searchQuery = '';
        sortBy = '';
    }

    function handlePageChange(page) {
        currentPage = page;
        console.log('Page changed to:', page);
    }
</script>

<div class="space-y-6">
    <!-- 브레드크럼 -->
    <Breadcrumb
        items={[
            { label: 'RMS', href: '/rms/user-dashboard' },
            { label: '기술자료' },
        ]}
    />

    <!-- 헤더 -->
    <PageHeader
        title="기술자료 목록"
        description="등록된 기술자료를 관리합니다."
        icon="FolderOpen"
    >
        <a href="/rms/digital-assets/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-2">
            <Icon name="FilePlus" size="sm" />
            새 기술자료
        </a>
    </PageHeader>

    <!-- 통계 카드 -->
    <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                    <Icon name="FolderOpen" size="sm" class="text-blue-600 dark:text-blue-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{statusCounts.all}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">전체</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-slate-100 dark:bg-slate-800 flex items-center justify-center">
                    <Icon name="FileEdit" size="sm" class="text-slate-600 dark:text-slate-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{statusCounts.draft}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">임시저장</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-amber-100 dark:bg-amber-900 flex items-center justify-center">
                    <Icon name="Clock" size="sm" class="text-amber-600 dark:text-amber-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{statusCounts.pending}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">승인대기</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                    <Icon name="Eye" size="sm" class="text-green-600 dark:text-green-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{statusCounts.published}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">등록</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-gray-100 dark:bg-gray-800 flex items-center justify-center">
                    <Icon name="EyeOff" size="sm" class="text-gray-600 dark:text-gray-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{statusCounts.unpublished}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">미등록</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-xl bg-red-100 dark:bg-red-900 flex items-center justify-center">
                    <Icon name="XCircle" size="sm" class="text-red-600 dark:text-red-400" />
                </div>
                <div>
                    <p class="text-xl font-bold text-gray-900 dark:text-white">{statusCounts.rejected}</p>
                    <p class="text-xs text-gray-500 dark:text-gray-400">반려</p>
                </div>
            </div>
        </div>
    </div>

    <!-- 테이블 카드 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <!-- 검색 및 필터 헤더 -->
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 space-y-4">
            <!-- 상태 탭 + 검색 -->
            <div class="flex flex-col lg:flex-row lg:items-center justify-between gap-4">
                <!-- 상태 필터 탭 -->
                <div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg overflow-x-auto">
                    <button
                        onclick={() => statusFilter = 'all'}
                        class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap {statusFilter === 'all' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        전체
                        <span class="ml-1 text-xs text-gray-400">({statusCounts.all})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'draft'}
                        class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap {statusFilter === 'draft' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        임시저장
                        <span class="ml-1 text-xs text-gray-400">({statusCounts.draft})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'pending'}
                        class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap flex items-center gap-1 {statusFilter === 'pending' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        승인대기
                        {#if statusCounts.pending > 0}
                            <span class="px-1.5 py-0.5 text-xs bg-amber-100 text-amber-700 dark:bg-amber-900 dark:text-amber-300 rounded-full">
                                {statusCounts.pending}
                            </span>
                        {/if}
                    </button>
                    <button
                        onclick={() => statusFilter = 'published'}
                        class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap {statusFilter === 'published' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        등록
                        <span class="ml-1 text-xs text-gray-400">({statusCounts.published})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'unpublished'}
                        class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap {statusFilter === 'unpublished' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        미등록
                        <span class="ml-1 text-xs text-gray-400">({statusCounts.unpublished})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'rejected'}
                        class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap {statusFilter === 'rejected' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        반려
                        <span class="ml-1 text-xs text-gray-400">({statusCounts.rejected})</span>
                    </button>
                </div>

                <!-- 검색 + 필터 버튼 -->
                <div class="flex items-center gap-2">
                    <!-- 검색창 -->
                    <div class="relative">
                        <Icon name="Search" size="sm" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
                        <input
                            type="text"
                            bind:value={searchQuery}
                            placeholder="제목, 등록인 검색..."
                            class="pl-10 pr-4 py-2 w-64 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-sm text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                        />
                        {#if searchQuery}
                            <button
                                onclick={() => searchQuery = ''}
                                class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
                            >
                                <Icon name="X" size="xs" />
                            </button>
                        {/if}
                    </div>

                    <!-- 필터 토글 버튼 -->
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

            <!-- 확장 필터 영역 -->
            {#if showFilters}
                <div class="pt-4 border-t border-gray-100 dark:border-gray-800 space-y-4">
                    <!-- 자료 타입 (복수 선택) -->
                    <div>
                        <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-2">자료 타입</label>
                        <div class="flex flex-wrap gap-2">
                            {#each assetTypeOptions as type}
                                <button
                                    onclick={() => toggleAssetType(type)}
                                    class="px-3 py-1.5 text-sm rounded-lg border transition-colors {selectedAssetTypes.includes(type) ? 'bg-blue-100 dark:bg-blue-900 border-blue-300 dark:border-blue-700 text-blue-700 dark:text-blue-300' : 'bg-white dark:bg-gray-800 border-gray-200 dark:border-gray-700 text-gray-600 dark:text-gray-400 hover:border-gray-300 dark:hover:border-gray-600'}"
                                >
                                    {#if selectedAssetTypes.includes(type)}
                                        <Icon name="Check" size="xs" class="inline mr-1" />
                                    {/if}
                                    {type}
                                </button>
                            {/each}
                        </div>
                    </div>

                    <!-- 전동기 타입 (복수 선택) -->
                    <div>
                        <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-2">전동기 타입</label>
                        <div class="flex flex-wrap gap-2">
                            {#each motorTypeOptions as type}
                                <button
                                    onclick={() => toggleMotorType(type)}
                                    class="px-3 py-1.5 text-sm rounded-lg border transition-colors {selectedMotorTypes.includes(type) ? 'bg-purple-100 dark:bg-purple-900 border-purple-300 dark:border-purple-700 text-purple-700 dark:text-purple-300' : 'bg-white dark:bg-gray-800 border-gray-200 dark:border-gray-700 text-gray-600 dark:text-gray-400 hover:border-gray-300 dark:hover:border-gray-600'}"
                                >
                                    {#if selectedMotorTypes.includes(type)}
                                        <Icon name="Check" size="xs" class="inline mr-1" />
                                    {/if}
                                    {type}
                                </button>
                            {/each}
                        </div>
                    </div>

                    <!-- 가격 범위 + 필터 초기화 -->
                    <div class="flex flex-wrap items-end gap-4">
                        <div class="flex-1 min-w-[200px] max-w-[300px]">
                            <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-2">가격 범위</label>
                            <div class="flex items-center gap-2">
                                <input
                                    type="number"
                                    bind:value={priceMin}
                                    placeholder="최소"
                                    class="w-full px-3 py-2 text-sm border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                                />
                                <span class="text-gray-400">~</span>
                                <input
                                    type="number"
                                    bind:value={priceMax}
                                    placeholder="최대"
                                    class="w-full px-3 py-2 text-sm border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                                />
                            </div>
                        </div>
                        <button
                            onclick={clearFilters}
                            class="px-4 py-2 text-sm font-medium text-gray-600 dark:text-gray-400 border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors flex items-center gap-2"
                        >
                            <Icon name="RotateCcw" size="sm" />
                            필터 초기화
                        </button>
                    </div>

                    <!-- 적용된 필터 태그 -->
                    {#if activeFilterCount() > 0 || searchQuery}
                        <div class="flex flex-wrap items-center gap-2 pt-3 border-t border-gray-100 dark:border-gray-800">
                            <span class="text-xs text-gray-500 dark:text-gray-400">적용된 필터:</span>
                            {#if searchQuery}
                                <span class="inline-flex items-center gap-1 px-2 py-1 text-xs bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300 rounded-full">
                                    검색: {searchQuery}
                                    <button onclick={() => searchQuery = ''} class="hover:text-gray-900 dark:hover:text-white">
                                        <Icon name="X" size="xs" />
                                    </button>
                                </span>
                            {/if}
                            {#if selectedAssetTypes.length > 0}
                                {#each selectedAssetTypes as type}
                                    <span class="inline-flex items-center gap-1 px-2 py-1 text-xs bg-blue-100 dark:bg-blue-900 text-blue-700 dark:text-blue-300 rounded-full">
                                        {type}
                                        <button onclick={() => toggleAssetType(type)} class="hover:text-blue-900 dark:hover:text-blue-100">
                                            <Icon name="X" size="xs" />
                                        </button>
                                    </span>
                                {/each}
                            {/if}
                            {#if selectedMotorTypes.length > 0}
                                {#each selectedMotorTypes as type}
                                    <span class="inline-flex items-center gap-1 px-2 py-1 text-xs bg-purple-100 dark:bg-purple-900 text-purple-700 dark:text-purple-300 rounded-full">
                                        {type}
                                        <button onclick={() => toggleMotorType(type)} class="hover:text-purple-900 dark:hover:text-purple-100">
                                            <Icon name="X" size="xs" />
                                        </button>
                                    </span>
                                {/each}
                            {/if}
                            {#if priceMin !== '' || priceMax !== ''}
                                <span class="inline-flex items-center gap-1 px-2 py-1 text-xs bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 rounded-full">
                                    가격: {priceMin || '0'}원 ~ {priceMax || '∞'}원
                                    <button onclick={() => { priceMin = ''; priceMax = ''; }} class="hover:text-green-900 dark:hover:text-green-100">
                                        <Icon name="X" size="xs" />
                                    </button>
                                </span>
                            {/if}
                        </div>
                    {/if}
                </div>
            {/if}
        </header>

        <!-- 정렬 옵션 -->
        <div class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center gap-4 overflow-x-auto">
            <span class="text-xs font-medium text-gray-500 dark:text-gray-400 whitespace-nowrap">정렬:</span>
            <div class="flex items-center gap-1">
                {#each sortOptions as option}
                    <button
                        onclick={() => toggleSort(option.value)}
                        class="px-3 py-1.5 text-xs font-medium rounded-md transition-colors whitespace-nowrap {sortBy === option.value ? 'bg-primary text-white' : 'text-gray-500 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        {option.label}
                    </button>
                {/each}
            </div>
            {#if sortBy}
                <button
                    onclick={() => sortBy = ''}
                    class="text-xs text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 flex items-center gap-1"
                >
                    <Icon name="X" size="xs" />
                    정렬 해제
                </button>
            {/if}
        </div>

        <!-- 테이블 -->
        <section class="p-6">
            <DigitalAssetTable
                data={filteredAssets()}
                onToggleStatus={handleToggleStatus}
                onDelete={handleDelete}
                emptyMessage="조건에 맞는 기술자료가 없습니다."
            />
        </section>

        <!-- 페이지네이션 -->
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