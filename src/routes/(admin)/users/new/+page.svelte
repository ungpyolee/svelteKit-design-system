<script>
    /**
     * 사용자 관리 - 생성 페이지
     *
     * [페이지 설명]
     * - 새 사용자를 추가하기 위한 폼 페이지
     * - 기본 정보 입력 + 역할/팀 배정 영역으로 구성
     */

    import Icon from "$lib/icons/icon.svelte";

    let formData = $state({
        displayName: '',
        email: '',
        phone: '',
        affiliation: '',
    });

    function handleSubmit(e) {
        e.preventDefault();
        const selectedPermissions = permissions.filter(p => p.checked).map(p => p.id);
        console.log('Create user:', { ...formData, permissions: selectedPermissions });
    }

    // 샘플 권한 데이터 (checked 필드 추가)
    let permissions = $state([
        {
            id: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            code: 'user:read',
            name: '사용자 조회',
            category: 'user',
            description: '사용자 목록 및 상세 정보를 조회할 수 있습니다.',
            checked: false
        },
        {
            id: '4fa85f64-5717-4562-b3fc-2c963f66afa7',
            code: 'user:write',
            name: '사용자 수정',
            category: 'user',
            description: '사용자 정보를 생성, 수정, 삭제할 수 있습니다.',
            checked: false
        },
        {
            id: '5fa85f64-5717-4562-b3fc-2c963f66afa8',
            code: 'asset:read',
            name: '자산 조회',
            category: 'asset',
            description: '디지털 자산 목록 및 상세 정보를 조회할 수 있습니다.',
            checked: true  // 기본 부여된 권한 예시
        },
        {
            id: '6fa85f64-5717-4562-b3fc-2c963f66afa9',
            code: 'asset:write',
            name: '자산 수정',
            category: 'asset',
            description: '디지털 자산을 생성, 수정, 삭제할 수 있습니다.',
            checked: false
        },
        {
            id: '7fa85f64-5717-4562-b3fc-2c963f66afaa',
            code: 'admin:full',
            name: '관리자 전체 권한',
            category: 'admin',
            description: '모든 관리자 기능에 접근할 수 있습니다.',
            checked: false
        },
    ]);

    // 카테고리별 스타일
    const categoryConfig = {
        'user': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400' },
        'asset': { class: 'bg-info-100 text-info-700 dark:bg-info-900/30 dark:text-info-400' },
        'admin': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        'team': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400' },
    };

    // ========== 권한 편집 모드 ==========
    let isEditingPermissions = $state(false);
    let originalPermissions = $state([]);  // 수정 취소 시 복원용

    function startEditPermissions() {
        // 현재 상태 백업
        originalPermissions = permissions.map(p => ({ ...p }));
        isEditingPermissions = true;
    }

    function cancelEditPermissions() {
        // 원래 상태로 복원
        permissions = originalPermissions.map(p => ({ ...p }));
        isEditingPermissions = false;
    }

    function savePermissions() {
        const selectedPermissions = permissions.filter(p => p.checked).map(p => p.id);
        console.log('Save permissions:', selectedPermissions);
        // TODO: API 호출
        isEditingPermissions = false;
    }

    // ========== 전체 선택/해제 ==========
    let allChecked = $derived(permissions.every(p => p.checked));
    let someChecked = $derived(permissions.some(p => p.checked) && !allChecked);

    function toggleAll() {
        const newValue = !allChecked;
        permissions = permissions.map(p => ({ ...p, checked: newValue }));
    }

    function togglePermission(id) {
        if (!isEditingPermissions) return;
        permissions = permissions.map(p => 
            p.id === id ? { ...p, checked: !p.checked } : p
        );
    }

    // 선택된 권한 수
    let selectedCount = $derived(permissions.filter(p => p.checked).length);
</script>

<!-- ==================== 메인 컨텐츠 ==================== -->
<div class="mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">

        <!-- ===== 페이지 헤더 ===== -->
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/admin/users"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">사용자 추가</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">새 사용자 정보를 입력하세요</p>
                </div>
            </div>
        </header>

        <!-- ===== 폼 영역 ===== -->
        <form onsubmit={handleSubmit} class="p-6 space-y-6">

            <!-- 기본 정보 섹션 -->
            <div class="space-y-4">
                <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">기본 정보</h3>

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

            <!-- ===== 권한 배정 섹션 ===== -->
            <div class="space-y-4">
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                    <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
                        <div class="flex items-center gap-3">
                            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">권한 목록</p>
                            {#if selectedCount > 0}
                                <span class="px-2 py-0.5 text-xs font-medium bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400 rounded-full">
                                    {selectedCount}개 선택됨
                                </span>
                            {/if}
                        </div>
                        
                        <!-- 수정/저장 버튼 -->
                        <div class="flex items-center gap-2">
                            {#if isEditingPermissions}
                                <button
                                    type="button"
                                    onclick={cancelEditPermissions}
                                    class="px-3 py-1.5 text-sm font-medium text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                >
                                    취소
                                </button>
                                <button
                                    type="button"
                                    onclick={savePermissions}
                                    class="px-3 py-1.5 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-1"
                                >
                                    <Icon name="Check" size="xs" />
                                    저장
                                </button>
                            {:else}
                                <button
                                    type="button"
                                    onclick={startEditPermissions}
                                    class="px-3 py-1.5 text-sm font-medium text-primary hover:bg-primary-50 dark:hover:bg-primary-900/20 rounded-lg transition-colors flex items-center gap-1"
                                >
                                    <Icon name="Pencil" size="xs" />
                                    수정
                                </button>
                            {/if}
                        </div>
                    </header>
                    
                    <section class="p-6">
                        <table class="w-full">
                            <thead>
                                <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                    <th class="px-4 py-3 w-12">
                                        <input 
                                            type="checkbox" 
                                            checked={allChecked}
                                            indeterminate={someChecked}
                                            onchange={toggleAll}
                                            disabled={!isEditingPermissions}
                                            class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary dark:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed"
                                        />
                                    </th>
                                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">권한명</th>
                                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">코드</th>
                                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">카테고리</th>
                                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">설명</th>
                                </tr>
                            </thead>
                            <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                                {#each permissions as permission}
                                    <tr
                                        class="transition-colors {isEditingPermissions ? 'hover:bg-gray-50 dark:hover:bg-gray-800/50 cursor-pointer' : ''} {permission.checked ? 'bg-primary-50/50 dark:bg-primary-900/10' : ''}"
                                        onclick={() => togglePermission(permission.id)}
                                    >
                                        <td class="px-4 py-3">
                                            <input 
                                                type="checkbox" 
                                                checked={permission.checked}
                                                onchange={() => togglePermission(permission.id)}
                                                onclick={(e) => e.stopPropagation()}
                                                disabled={!isEditingPermissions}
                                                class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary dark:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed"
                                            />
                                        </td>
                                        <td class="px-4 py-3">
                                            <div class="flex items-center gap-3">
                                                <div class="w-8 h-8 rounded-lg bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-600 dark:text-gray-400">
                                                    <Icon name="Shield" size="sm"/>
                                                </div>
                                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{permission.name}</span>
                                            </div>
                                        </td>
                                        <td class="px-4 py-3">
                                            <code class="text-xs font-mono text-gray-600 dark:text-gray-400 bg-gray-100 dark:bg-gray-800 px-2 py-1 rounded">
                                                {permission.code}
                                            </code>
                                        </td>
                                        <td class="px-4 py-3">
                                            <span class="px-2 py-1 text-xs font-medium {categoryConfig[permission.category]?.class || 'bg-gray-100 text-gray-700'} rounded-full">
                                                {permission.category}
                                            </span>
                                        </td>
                                        <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400 max-w-[250px] truncate">
                                            {permission.description}
                                        </td>
                                    </tr>
                                {/each}
                            </tbody>
                        </table>
                    </section>
                </div>
            </div>

            <!-- ===== 하단 버튼 영역 ===== -->
            <div class="flex items-center justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                <a
                    href="/admin/users"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </a>
                <button
                    type="submit"
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                    사용자 추가
                </button>
            </div>
        </form>
    </div>
</div>