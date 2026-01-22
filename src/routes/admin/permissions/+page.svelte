<script>
    /**
     * 권한 관리 - 목록 페이지
     * 시스템 레벨 권한(Permission)을 관리하는 페이지
     */
    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";

    // 샘플 권한 데이터
    let permissions = $state([
        {
            id: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            code: 'user:read',
            name: '사용자 조회',
            category: 'user',
            description: '사용자 목록 및 상세 정보를 조회할 수 있습니다.',
            createdAt: '2024-01-15T09:00:00.000Z'
        },
        {
            id: '4fa85f64-5717-4562-b3fc-2c963f66afa7',
            code: 'user:write',
            name: '사용자 수정',
            category: 'user',
            description: '사용자 정보를 생성, 수정, 삭제할 수 있습니다.',
            createdAt: '2024-01-15T09:00:00.000Z'
        },
        {
            id: '5fa85f64-5717-4562-b3fc-2c963f66afa8',
            code: 'asset:read',
            name: '자산 조회',
            category: 'asset',
            description: '디지털 자산 목록 및 상세 정보를 조회할 수 있습니다.',
            createdAt: '2024-01-20T10:30:00.000Z'
        },
        {
            id: '6fa85f64-5717-4562-b3fc-2c963f66afa9',
            code: 'asset:write',
            name: '자산 수정',
            category: 'asset',
            description: '디지털 자산을 생성, 수정, 삭제할 수 있습니다.',
            createdAt: '2024-01-20T10:30:00.000Z'
        },
        {
            id: '7fa85f64-5717-4562-b3fc-2c963f66afaa',
            code: 'admin:full',
            name: '관리자 전체 권한',
            category: 'admin',
            description: '모든 관리자 기능에 접근할 수 있습니다.',
            createdAt: '2024-02-01T14:00:00.000Z'
        },
    ]);

    // 카테고리별 스타일
    const categoryConfig = {
        'user': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400' },
        'asset': { class: 'bg-info-100 text-info-700 dark:bg-info-900/30 dark:text-info-400' },
        'admin': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        'team': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400' },
    };

    // 모달 상태
    let showDetailModal = $state(false);
    let selectedPermission = $state(null);

    // 페이지네이션
    let currentPage = $state(1);
    const totalItems = 24;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }

    function handleRowClick(permission) {
        selectedPermission = permission;
        showDetailModal = true;
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <!-- 헤더 -->
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">권한 관리</p>
            <a href="/admin/permissions/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                <Icon name="Plus" size="sm" class="inline mr-1" />
                권한 추가
            </a>
        </header>

        <!-- 테이블 -->
        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">권한명</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">코드</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">카테고리</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">설명</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">생성일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each permissions as permission}
                        <tr
                            class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                            onclick={() => handleRowClick(permission)}
                        >
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
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400 max-w-[200px] truncate">
                                {permission.description}
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(permission.createdAt)}
                            </td>
                            <td class="px-4 py-3">
                                <button
                                    class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                    onclick={(e) => { e.stopPropagation(); handleRowClick(permission); }}
                                >
                                    <Icon name="ChevronRight" size="sm"/>
                                </button>
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>

        <!-- 페이지네이션 -->
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

<!-- 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="권한 정보" size="md">
    {#if selectedPermission}
        <div class="space-y-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-lg bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-600 dark:text-gray-400">
                    <Icon name="Shield" size="md"/>
                </div>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedPermission.name}</p>
                    <code class="text-xs font-mono text-gray-500">{selectedPermission.code}</code>
                </div>
            </div>

            <div class="grid grid-cols-2 gap-4">
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">카테고리</p>
                    <span class="px-2 py-1 text-xs font-medium {categoryConfig[selectedPermission.category]?.class} rounded-full">
                        {selectedPermission.category}
                    </span>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">생성일</p>
                    <p class="text-sm text-gray-800 dark:text-gray-200">{formatDate(selectedPermission.createdAt)}</p>
                </div>
            </div>

            <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">설명</p>
                <p class="text-sm text-gray-800 dark:text-gray-200">{selectedPermission.description}</p>
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
            href="/admin/permissions/{selectedPermission?.id}/edit"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            수정
        </a>
    {/snippet}
</Modal>
