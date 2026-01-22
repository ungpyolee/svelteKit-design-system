<script>
    /**
     * 테넌트(팀) 관리 - 목록 페이지
     */
    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";

    let tenants = $state([
        {
            id: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            name: 'CLEW',
            slug: 'clew',
            status: 'Active',
            createdBy: '김철수',
            createdAt: '2024-01-15T09:00:00.000Z',
            memberCount: 12
        },
        {
            id: '4fa85f64-5717-4562-b3fc-2c963f66afa7',
            name: 'GENEERS',
            slug: 'geneers',
            status: 'Active',
            createdBy: '이민수',
            createdAt: '2024-02-20T10:30:00.000Z',
            memberCount: 8
        },
        {
            id: '5fa85f64-5717-4562-b3fc-2c963f66afa8',
            name: 'Motorspace',
            slug: 'motorspace',
            status: 'Suspended',
            createdBy: '박지영',
            createdAt: '2024-03-10T14:20:00.000Z',
            memberCount: 5
        },
    ]);

    const statusConfig = {
        'Active': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400', dot: 'bg-success-500' },
        'Suspended': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400', dot: 'bg-warning-500' },
        'Inactive': { class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400', dot: 'bg-gray-400' },
    };

    let showDetailModal = $state(false);
    let selectedTenant = $state(null);

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

    function handleRowClick(tenant) {
        selectedTenant = tenant;
        showDetailModal = true;
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">테넌트 관리</p>
            <a href="/admin/tenants/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                <Icon name="Plus" size="sm" class="inline mr-1" />
                테넌트 추가
            </a>
        </header>

        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">테넌트</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Slug</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">멤버 수</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">생성일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each tenants as tenant}
                        <tr
                            class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                            onclick={() => handleRowClick(tenant)}
                        >
                            <td class="px-4 py-3">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold">
                                        {tenant.name.charAt(0)}
                                    </div>
                                    <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{tenant.name}</span>
                                </div>
                            </td>
                            <td class="px-4 py-3">
                                <code class="text-xs font-mono text-gray-600 dark:text-gray-400 bg-gray-100 dark:bg-gray-800 px-2 py-1 rounded">
                                    {tenant.slug}
                                </code>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {tenant.memberCount}명
                            </td>
                            <td class="px-4 py-3">
                                <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium {statusConfig[tenant.status]?.class} rounded-full">
                                    <span class="w-1.5 h-1.5 {statusConfig[tenant.status]?.dot} rounded-full"></span>
                                    {tenant.status}
                                </span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(tenant.createdAt)}
                            </td>
                            <td class="px-4 py-3">
                                <button
                                    class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                    onclick={(e) => { e.stopPropagation(); handleRowClick(tenant); }}
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
            <Pagination bind:currentPage={currentPage} {totalPages} {totalItems} {itemsPerPage} />
        </footer>
    </div>
</div>

<!-- 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="테넌트 정보" size="md">
    {#if selectedTenant}
        <div class="space-y-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold">
                    {selectedTenant.name.charAt(0)}
                </div>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedTenant.name}</p>
                    <code class="text-xs font-mono text-gray-500">{selectedTenant.slug}</code>
                </div>
            </div>

            <div class="grid grid-cols-2 gap-4">
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">상태</p>
                    <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium {statusConfig[selectedTenant.status]?.class} rounded-full">
                        <span class="w-1.5 h-1.5 {statusConfig[selectedTenant.status]?.dot} rounded-full"></span>
                        {selectedTenant.status}
                    </span>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">멤버 수</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedTenant.memberCount}명</p>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">생성자</p>
                    <p class="text-sm text-gray-800 dark:text-gray-200">{selectedTenant.createdBy}</p>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">생성일</p>
                    <p class="text-sm text-gray-800 dark:text-gray-200">{formatDate(selectedTenant.createdAt)}</p>
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
            href="/admin/tenants/{selectedTenant?.id}/edit"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            상세/수정
        </a>
    {/snippet}
</Modal>
