<!-- /routes/(apps)/rms-admin/TypeManageTable.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";

    /**
     * @typedef {Object} TypeItem
     * @property {string|number} typeId - 타입 ID
     * @property {string} createdDate - 등록일
     * @property {string} name - 타입 이름
     * @property {string} [description] - 설명
     * @property {string} authorName - 등록인 이름
     * @property {string} authorEmail - 등록인 이메일
     * @property {boolean} isActive - 사용 상태
     */

    let {
        data = [],
        typeLabel = '타입',
        onToggleStatus = () => {},
        onDelete = () => {},
        emptyMessage = '등록된 항목이 없습니다.',
    } = $props();

    // 상세 모달 상태
    let showDetailModal = $state(false);
    let selectedItem = $state(null);

    // 삭제 확인 모달 상태
    let showDeleteConfirm = $state(false);
    let deleteTarget = $state(null);

    function handleRowClick(item) {
        selectedItem = item;
        showDetailModal = true;
    }

    function handleToggleStatus(item, e) {
        e.stopPropagation();
        onToggleStatus(item);
    }

    function handleDeleteClick(item, e) {
        e.stopPropagation();
        deleteTarget = item;
        showDeleteConfirm = true;
    }

    function confirmDelete() {
        if (deleteTarget) {
            onDelete(deleteTarget);
        }
        showDeleteConfirm = false;
        deleteTarget = null;
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">{typeLabel} 이름</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록인</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-center">상태</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-28"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as item}
                <tr
                    class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(item)}
                >
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                <Icon name="Tag" size="sm"/>
                            </div>
                            <div>
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">
                                    {item.name}
                                </span>
                                {#if item.description}
                                    <p class="text-xs text-gray-500 dark:text-gray-400 truncate max-w-[200px]">
                                        {item.description}
                                    </p>
                                {/if}
                            </div>
                        </div>
                    </td>
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {item.createdDate}
                    </td>
                    <td class="px-4 py-3">
                        <div class="text-left">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{item.authorName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{item.authorEmail}</p>
                        </div>
                    </td>
                    <td class="px-4 py-3 text-center">
                        <button
                            onclick={(e) => handleToggleStatus(item, e)}
                            class="inline-flex items-center gap-1.5 px-2.5 py-1 text-xs font-medium rounded-full transition-colors {item.isActive ? 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400 hover:bg-success-200 dark:hover:bg-success-800' : 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400 hover:bg-gray-200 dark:hover:bg-gray-700'}"
                        >
                            {#if item.isActive}
                                <Icon name="Check" size="xs" />
                                사용
                            {:else}
                                <Icon name="X" size="xs" />
                                미사용
                            {/if}
                        </button>
                    </td>
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-1 justify-end">
                            <button
                                onclick={(e) => handleDeleteClick(item, e)}
                                class="p-1.5 text-gray-400 hover:text-red-600 dark:hover:text-red-400 hover:bg-red-50 dark:hover:bg-red-900 rounded-lg transition-colors"
                                title="삭제"
                            >
                                <Icon name="Trash2" size="sm"/>
                            </button>
                            <button
                                class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                onclick={(e) => { e.stopPropagation(); handleRowClick(item); }}
                                title="상세보기"
                            >
                                <Icon name="ChevronRight" size="sm"/>
                            </button>
                        </div>
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
{:else}
    <div class="text-center py-12">
        <div class="w-16 h-16 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
            <Icon name="Tag" size="lg" />
        </div>
        <p class="text-gray-500 dark:text-gray-400">{emptyMessage}</p>
    </div>
{/if}

<!-- 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="{typeLabel} 상세" size="md">
    {#if selectedItem}
        <div class="space-y-6">
            <!-- 타입 정보 헤더 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="Tag" size="md"/>
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedItem.name}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">ID: {selectedItem.typeId}</p>
                </div>
                <span class="px-2 py-1 text-xs font-medium rounded-full {selectedItem.isActive ? 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400' : 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400'}">
                    {selectedItem.isActive ? '사용' : '미사용'}
                </span>
            </div>

            <!-- 설명 -->
            {#if selectedItem.description}
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-2">설명</p>
                    <p class="text-sm text-gray-700 dark:text-gray-300">{selectedItem.description}</p>
                </div>
            {/if}

            <!-- 상세 정보 -->
            <div class="grid grid-cols-2 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedItem.createdDate}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">상태</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedItem.isActive ? '사용' : '미사용'}</p>
                </div>
            </div>

            <!-- 등록인 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">등록인 정보</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                        {selectedItem.authorName?.charAt(0) || '?'}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedItem.authorName}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedItem.authorEmail}</p>
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
        <button
            onclick={() => { onToggleStatus(selectedItem); showDetailModal = false; }}
            class="px-4 py-2 text-sm font-medium text-white rounded-lg transition-colors {selectedItem?.isActive ? 'bg-gray-500 hover:bg-gray-600' : 'bg-success hover:bg-success-600'}"
        >
            {selectedItem?.isActive ? '미사용으로 변경' : '사용으로 변경'}
        </button>
    {/snippet}
</Modal>

<!-- 삭제 확인 모달 -->
{#if showDeleteConfirm}
    <div class="fixed inset-0 z-50 flex items-center justify-center">
        <div 
            class="absolute inset-0 bg-black/50"
            onclick={() => showDeleteConfirm = false}
        ></div>
        <div class="relative bg-white dark:bg-gray-900 rounded-xl shadow-xl max-w-md w-full mx-4 p-6">
            <div class="flex items-center gap-4 mb-4">
                <div class="w-12 h-12 rounded-full bg-red-100 dark:bg-red-900 flex items-center justify-center text-red-600 dark:text-red-400">
                    <Icon name="AlertTriangle" size="md" />
                </div>
                <div>
                    <h3 class="text-lg font-medium text-gray-900 dark:text-white">{typeLabel} 삭제</h3>
                    <p class="text-sm text-gray-500 dark:text-gray-400">이 작업은 되돌릴 수 없습니다.</p>
                </div>
            </div>
            
            <p class="text-sm text-gray-600 dark:text-gray-300 mb-6">
                "<span class="font-medium">{deleteTarget?.name}</span>" {typeLabel}을(를) 삭제하시겠습니까?
            </p>
            
            <div class="flex justify-end gap-3">
                <button
                    onclick={() => showDeleteConfirm = false}
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </button>
                <button
                    onclick={confirmDelete}
                    class="px-4 py-2 text-sm font-medium bg-red-600 text-white rounded-lg hover:bg-red-700 transition-colors"
                >
                    삭제
                </button>
            </div>
        </div>
    </div>
{/if}