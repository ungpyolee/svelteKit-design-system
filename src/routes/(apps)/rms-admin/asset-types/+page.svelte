<!-- /routes/(apps)/rms-admin/asset-types/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import Modal from "$lib/components/ui/Modal.svelte";
    import TypeManageTable from "../TypeManageTable.svelte";

    // 더미 데이터
    let assetTypes = $state([
        {
            typeId: 'AT-001',
            createdDate: '2024-01-15',
            name: '설계도면',
            description: '전동기 설계에 필요한 CAD 도면, 회로도 등의 기술 자료입니다.',
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            isActive: true
        },
        {
            typeId: 'AT-002',
            createdDate: '2024-01-15',
            name: '사양서',
            description: '전동기의 기술 사양, 성능 스펙 등을 정리한 문서입니다.',
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            isActive: true
        },
        {
            typeId: 'AT-004',
            createdDate: '2024-01-15',
            name: '기술문서',
            description: '전동기 관련 기술 매뉴얼, 가이드, 교육 자료 등입니다.',
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            isActive: true
        },
        {
            typeId: 'AT-005',
            createdDate: '2024-01-15',
            name: '해석보고서',
            description: '전동기 전자기 해석, 열 해석, 구조 해석 결과 보고서입니다.',
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            isActive: true
        },
        {
            typeId: 'AT-006',
            createdDate: '2024-01-10',
            name: '연구보고서',
            description: '전동기 관련 연구 결과 및 논문 자료입니다.',
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            isActive: false
        },
    ]);

    // 필터 상태
    let statusFilter = $state('all');

    // 필터링된 데이터
    let filteredTypes = $derived(
        statusFilter === 'all'
            ? assetTypes
            : statusFilter === 'active'
                ? assetTypes.filter(t => t.isActive)
                : assetTypes.filter(t => !t.isActive)
    );

    // 상태별 건수
    let activeCount = $derived(assetTypes.filter(t => t.isActive).length);
    let inactiveCount = $derived(assetTypes.filter(t => !t.isActive).length);

    // 페이지네이션
    let currentPage = $state(1);
    const itemsPerPage = 10;
    let totalPages = $derived(Math.ceil(filteredTypes.length / itemsPerPage));

    // 신규 등록 모달
    let showAddModal = $state(false);
    let newTypeForm = $state({
        name: '',
        description: ''
    });
    let isSubmitting = $state(false);

    // 유효성 검사
    let isFormValid = $derived(newTypeForm.name.trim() !== '');

    // 이벤트 핸들러
    function handleToggleStatus(item) {
        const index = assetTypes.findIndex(t => t.typeId === item.typeId);
        if (index !== -1) {
            assetTypes[index].isActive = !assetTypes[index].isActive;
        }
        console.log('Toggled status:', item.typeId);
    }

    function handleDelete(item) {
        assetTypes = assetTypes.filter(t => t.typeId !== item.typeId);
        console.log('Deleted:', item.typeId);
    }

    async function handleAddSubmit() {
        if (!isFormValid) return;
        
        isSubmitting = true;
        
        // API 호출 시뮬레이션
        await new Promise(resolve => setTimeout(resolve, 1000));
        
        // 새 타입 추가
        const newType = {
            typeId: `AT-${String(assetTypes.length + 1).padStart(3, '0')}`,
            createdDate: new Date().toISOString().split('T')[0],
            name: newTypeForm.name,
            description: newTypeForm.description,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            isActive: true
        };
        
        assetTypes = [newType, ...assetTypes];
        
        // 폼 초기화
        newTypeForm = { name: '', description: '' };
        isSubmitting = false;
        showAddModal = false;
        
        console.log('Added:', newType);
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리', href: '/rms-admin/dashboard' },
            { label: '자료 타입 관리' }
        ]} 
    />

    <PageHeader 
        title="자료 타입 관리" 
        description="기술자료의 타입을 관리합니다."
        icon="Tags"
    >
        <button
            onclick={() => showAddModal = true}
            class="px-4 py-2 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-2"
        >
            <Icon name="Plus" size="sm" />
            신규 타입 등록
        </button>
    </PageHeader>

    <!-- 통계 카드 -->
    <div class="grid grid-cols-3 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-blue-100 dark:bg-blue-900 flex items-center justify-center">
                    <Icon name="Tags" size="md" class="text-blue-600 dark:text-blue-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{assetTypes.length}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">전체 타입</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                    <Icon name="Check" size="md" class="text-green-600 dark:text-green-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{activeCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">사용</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-gray-100 dark:bg-gray-800 flex items-center justify-center">
                    <Icon name="X" size="md" class="text-gray-600 dark:text-gray-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{inactiveCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">미사용</p>
                </div>
            </div>
        </div>
    </div>

    <!-- 필터 탭 + 테이블 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center justify-between">
                <div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg">
                    <button
                        onclick={() => statusFilter = 'all'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'all' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        전체
                        <span class="ml-1 text-xs text-gray-400">({assetTypes.length})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'active'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'active' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        사용
                        <span class="ml-1 text-xs text-gray-400">({activeCount})</span>
                    </button>
                    <button
                        onclick={() => statusFilter = 'inactive'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {statusFilter === 'inactive' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        미사용
                        <span class="ml-1 text-xs text-gray-400">({inactiveCount})</span>
                    </button>
                </div>
            </div>
        </header>
        
        <section class="p-6">
            <TypeManageTable 
                data={filteredTypes}
                typeLabel="자료 타입"
                onToggleStatus={handleToggleStatus}
                onDelete={handleDelete}
                emptyMessage={statusFilter === 'active' ? '사용 중인 자료 타입이 없습니다.' : statusFilter === 'inactive' ? '미사용 자료 타입이 없습니다.' : '등록된 자료 타입이 없습니다.'}
            />
        </section>
        
        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination 
                bind:currentPage={currentPage}
                totalPages={totalPages}
                totalItems={filteredTypes.length}
                itemsPerPage={itemsPerPage}
            />
        </footer>
    </div>
</div>

<!-- 신규 등록 모달 -->
<Modal bind:isOpen={showAddModal} title="신규 자료 타입 등록" size="md">
    <div class="space-y-4">
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                타입 이름 <span class="text-danger">*</span>
            </label>
            <input
                type="text"
                bind:value={newTypeForm.name}
                placeholder="자료 타입 이름을 입력하세요"
                class="w-full px-4 py-3 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
            />
        </div>
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                설명
            </label>
            <textarea
                bind:value={newTypeForm.description}
                rows="4"
                placeholder="자료 타입에 대한 설명을 입력하세요 (선택)"
                class="w-full px-4 py-3 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
            ></textarea>
        </div>
    </div>

    {#snippet footer()}
        <button
            onclick={() => { showAddModal = false; newTypeForm = { name: '', description: '' }; }}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button
            onclick={handleAddSubmit}
            disabled={!isFormValid || isSubmitting}
            class="px-6 py-2 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-2"
        >
            {#if isSubmitting}
                <div class="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                등록 중...
            {:else}
                <Icon name="Check" size="sm" />
                등록
            {/if}
        </button>
    {/snippet}
</Modal>