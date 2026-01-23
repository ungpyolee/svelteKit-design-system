<script>
    /**
     * 사용자 관리 - 목록 페이지
     *
     * [페이지 설명]
     * - 전체 사용자 목록을 테이블 형태로 보여주는 페이지
     * - 행 클릭 시 빠른 상세 모달 표시
     * - 우측 상단에 "사용자 추가" 버튼 → 모달로 표시
     */

    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";
    import UserDetailModal from "./UserDetailModal.svelte";

    let users = $state([
        {
            userId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            externalId: 'google-oauth2|123456789',
            displayName: '김철수',
            email: 'cs.kim@company.com',
            phone: '010-1234-5678',
            affiliation: 'CLEW',
            defaultTenantId: 'tenant_001',
            status: 'Active',
            createdAt: '2024-01-15T09:00:00.000Z'
        },
        {
            userId: '4fa85f64-5717-4562-b3fc-2c963f66afa7',
            externalId: 'google-oauth2|987654321',
            displayName: '이민수',
            email: 'ms.lee@company.com',
            phone: '010-2345-6789',
            affiliation: 'GENEERS',
            defaultTenantId: 'tenant_002',
            status: 'Active',
            createdAt: '2024-02-20T10:30:00.000Z'
        },
        {
            userId: '5fa85f64-5717-4562-b3fc-2c963f66afa8',
            externalId: 'azure-ad|456789123',
            displayName: '박지영',
            email: 'jy.park@company.com',
            phone: '010-3456-7890',
            affiliation: 'Motorspace',
            defaultTenantId: 'tenant_001',
            status: 'Inactive',
            createdAt: '2024-03-10T14:20:00.000Z'
        },
    ]);

    // 상태별 스타일
    const statusConfig = {
        'Active': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400', dot: 'bg-success-500' },
        'Inactive': { class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400', dot: 'bg-gray-400' },
        'Pending': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400', dot: 'bg-warning-500' },
    };

    // ========== 검색 ==========
    let searchType = $state('email');
    let searchQuery = $state('');

    function handleSearch(e) {
        e.preventDefault();
        console.log('Search:', searchType, searchQuery);
    }

    // ========== 모달 상태 ==========
    let showDetailModal = $state(false);
    let showCreateModal = $state(false);
    let selectedUser = $state(null);

    // ========== 사용자 추가 폼 ==========
    let formData = $state({
        displayName: '',
        email: '',
        phone: '',
        affiliation: '',
    });

    function resetForm() {
        formData = {
            displayName: '',
            email: '',
            phone: '',
            affiliation: '',
        };
    }

    function handleCreateUser(e) {
        e.preventDefault();
        console.log('Create user:', formData);
        // TODO: API 호출
        showCreateModal = false;
        resetForm();
    }

    // ========== 페이지네이션 ==========
    let currentPage = $state(1);
    const totalItems = 156;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }

    function handleRowClick(user) {
        selectedUser = user;
        showDetailModal = true;
    }
</script>

<!-- ==================== 메인 컨텐츠 ==================== -->
<div class="grid grid-cols-1 gap-4">

    <!-- 사용자 테이블 카드 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">

        <!-- ===== 테이블 헤더 ===== -->
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">사용자 관리</p>
            <div class="flex items-center gap-2">
                <!-- 검색 -->
                <form onsubmit={handleSearch} class="flex items-center gap-2">
                    <select
                        bind:value={searchType}
                        class="px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    >
                        <option value="email">이메일</option>
                        <option value="id">ID</option>
                    </select>
                    <input
                        type="text"
                        bind:value={searchQuery}
                        placeholder={searchType === 'email' ? 'user@example.com' : 'User ID'}
                        class="w-64 px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                    <button
                        type="submit"
                        class="p-2 text-gray-500 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        <Icon name="Search" size="sm" />
                    </button>
                </form>
                <!-- 사용자 추가 버튼 -->
                <button 
                    onclick={() => showCreateModal = true}
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center"
                >
                    <Icon name="UserPlus" size="sm" class="inline mr-1" />
                    사용자 추가
                </button>
            </div>
        </header>

        <!-- ===== 테이블 본문 ===== -->
        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">사용자</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">소속</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">연락처</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">가입일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each users as user}
                        <tr
                            class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                            onclick={() => handleRowClick(user)}
                        >
                            <td class="px-4 py-3">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 rounded-full bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-medium">
                                        {user.displayName.charAt(0)}
                                    </div>
                                    <div>
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{user.displayName}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400">{user.email}</p>
                                    </div>
                                </div>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {user.affiliation}
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {user.phone}
                            </td>
                            <td class="px-4 py-3">
                                <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium {statusConfig[user.status]?.class} rounded-full">
                                    <span class="w-1.5 h-1.5 {statusConfig[user.status]?.dot} rounded-full"></span>
                                    {user.status}
                                </span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(user.createdAt)}
                            </td>
                            <td class="px-4 py-3">
                                <button
                                    class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                    onclick={(e) => { e.stopPropagation(); handleRowClick(user); }}
                                >
                                    <Icon name="ChevronRight" size="sm"/>
                                </button>
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>

        <!-- ===== 테이블 푸터 (페이지네이션) ===== -->
        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination
                bind:currentPage={currentPage}
                {totalPages}
                {totalItems}
                {itemsPerPage}
            />
        </footer>
    </div>
</div>


<!-- 사용자 상세 모달 -->
<UserDetailModal bind:isOpen={showDetailModal} user={selectedUser} />


<!-- 사용자 추가 모달 -->
<Modal bind:isOpen={showCreateModal} title="사용자 추가" size="md">
    <form id="createUserForm" onsubmit={handleCreateUser} class="space-y-6">
        
        <!-- 기본 정보 섹션 -->
        <div class="space-y-4">
            <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">기본 정보</h3>

            <!-- 이름 + 이메일 (2열) -->
            <div class="grid grid-cols-2 gap-4">
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        이름 <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="text"
                        bind:value={formData.displayName}
                        placeholder="홍길동"
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                </div>
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        이메일 <span class="text-red-500">*</span>
                    </label>
                    <input
                        type="email"
                        bind:value={formData.email}
                        placeholder="user@company.com"
                        required
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                </div>
            </div>

            <!-- 연락처 + 소속 (2열) -->
            <div class="grid grid-cols-2 gap-4">
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        연락처
                    </label>
                    <input
                        type="tel"
                        bind:value={formData.phone}
                        placeholder="010-1234-5678"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                </div>
                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        소속
                    </label>
                    <input
                        type="text"
                        bind:value={formData.affiliation}
                        placeholder="회사/조직명"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                </div>
            </div>
        </div>

        <!-- 역할 & 팀 배정 섹션 -->
        <div class="space-y-4">
            <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">역할 & 팀 배정</h3>
            <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                <p class="text-sm text-gray-500 dark:text-gray-400 text-center">역할 선택 및 팀 배정 UI 영역</p>
            </div>
        </div>
    </form>

    {#snippet footer()}
        <button 
            type="button"
            onclick={() => { showCreateModal = false; resetForm(); }}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button 
            type="submit"
            form="createUserForm"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            사용자 추가
        </button>
    {/snippet}
</Modal>