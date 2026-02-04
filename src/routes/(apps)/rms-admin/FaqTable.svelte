<!-- /routes/(apps)/rms-admin/FaqTable.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";

    let {
        data = [],
        onEdit = () => {},
        onDelete = () => {},
        onTogglePublish = () => {},
        emptyMessage = '등록된 FAQ가 없습니다.',
        questionMaxWidth = '300px',
    } = $props();

    // 상세 모달 상태
    let showDetailModal = $state(false);
    let selectedFaq = $state(null);

    // 삭제 확인 모달 상태
    let showDeleteConfirm = $state(false);
    let faqToDelete = $state(null);

    function handleRowClick(faq) {
        selectedFaq = faq;
        showDetailModal = true;
    }

    function handleEditClick(faq, e) {
        e?.stopPropagation();
        onEdit(faq);
    }

    function handleDeleteClick(faq, e) {
        e?.stopPropagation();
        faqToDelete = faq;
        showDeleteConfirm = true;
    }

    function confirmDelete() {
        if (faqToDelete) {
            onDelete(faqToDelete);
        }
        showDeleteConfirm = false;
        faqToDelete = null;
        
        // 상세 모달이 열려있으면 닫기
        if (selectedFaq?.faqId === faqToDelete?.faqId) {
            showDetailModal = false;
            selectedFaq = null;
        }
    }

    function handleTogglePublish(faq, e) {
        e?.stopPropagation();
        onTogglePublish(faq);
    }

    // 모달에서 수정 클릭
    function handleEditFromModal() {
        showDetailModal = false;
        onEdit(selectedFaq);
    }

    // 모달에서 삭제 클릭
    function handleDeleteFromModal() {
        showDetailModal = false;
        faqToDelete = selectedFaq;
        showDeleteConfirm = true;
    }

    // 모달에서 게시 상태 토글
    function handleToggleFromModal() {
        onTogglePublish(selectedFaq);
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">FAQ 질문</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록인</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 text-center">게시상태</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-28"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as faq}
                <tr 
                    class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(faq)}
                >
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-purple-100 dark:bg-purple-900 flex items-center justify-center text-purple-600 dark:text-purple-400 flex-shrink-0">
                                <Icon name="HelpCircle" size="sm"/>
                            </div>
                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate" style="max-width: {questionMaxWidth}">
                                {faq.question}
                            </span>
                        </div>
                    </td>
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {faq.createdDate}
                    </td>
                    <td class="px-4 py-3">
                        <div class="text-left">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{faq.authorName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{faq.authorEmail}</p>
                        </div>
                    </td>
                    <td class="px-4 py-3 text-center">
                        <button
                            onclick={(e) => handleTogglePublish(faq, e)}
                            class="inline-flex items-center gap-1.5 px-2.5 py-1 text-xs font-medium rounded-full transition-colors {faq.isPublished ? 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400 hover:bg-success-200 dark:hover:bg-success-800' : 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400 hover:bg-gray-200 dark:hover:bg-gray-700'}"
                        >
                            {#if faq.isPublished}
                                <Icon name="Eye" size="xs" />
                                게시중
                            {:else}
                                <Icon name="EyeOff" size="xs" />
                                비공개
                            {/if}
                        </button>
                    </td>
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-1 justify-end">
                            <button
                                onclick={(e) => handleEditClick(faq, e)}
                                class="p-1.5 text-gray-400 hover:text-blue-600 dark:hover:text-blue-400 hover:bg-blue-50 dark:hover:bg-blue-900 rounded-lg transition-colors"
                                title="수정"
                            >
                                <Icon name="Pencil" size="sm"/>
                            </button>
                            <button
                                onclick={(e) => handleDeleteClick(faq, e)}
                                class="p-1.5 text-gray-400 hover:text-red-600 dark:hover:text-red-400 hover:bg-red-50 dark:hover:bg-red-900 rounded-lg transition-colors"
                                title="삭제"
                            >
                                <Icon name="Trash2" size="sm"/>
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
            <Icon name="HelpCircle" size="lg" />
        </div>
        <p class="text-gray-500 dark:text-gray-400">{emptyMessage}</p>
    </div>
{/if}

<!-- FAQ 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="FAQ 상세" size="lg">
    {#if selectedFaq}
        <div class="space-y-6">
            <!-- 헤더 정보 -->
            <div class="flex items-start justify-between">
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-lg bg-purple-100 dark:bg-purple-900 flex items-center justify-center text-purple-600 dark:text-purple-400">
                        <Icon name="HelpCircle" size="md"/>
                    </div>
                    <div>
                        <p class="text-xs text-gray-500 dark:text-gray-400">FAQ ID</p>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedFaq.faqId}</p>
                    </div>
                </div>
                <button
                    onclick={handleToggleFromModal}
                    class="inline-flex items-center gap-1.5 px-3 py-1.5 text-xs font-medium rounded-full transition-colors {selectedFaq.isPublished ? 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400 hover:bg-success-200 dark:hover:bg-success-800' : 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400 hover:bg-gray-200 dark:hover:bg-gray-700'}"
                >
                    {#if selectedFaq.isPublished}
                        <Icon name="Eye" size="xs" />
                        게시중
                    {:else}
                        <Icon name="EyeOff" size="xs" />
                        비공개
                    {/if}
                </button>
            </div>

            <!-- 질문 -->
            <div class="p-4 bg-purple-50 dark:bg-purple-900/20 border border-purple-100 dark:border-purple-800 rounded-lg">
                <div class="flex items-start gap-3">
                    <span class="flex-shrink-0 w-6 h-6 rounded-full bg-purple-500 text-white flex items-center justify-center text-sm font-bold">Q</span>
                    <p class="text-gray-800 dark:text-gray-200 leading-relaxed">{selectedFaq.question}</p>
                </div>
            </div>

            <!-- 답변 -->
            <div class="p-4 bg-gray-50 dark:bg-gray-800 border border-gray-100 dark:border-gray-700 rounded-lg">
                <div class="flex items-start gap-3">
                    <span class="flex-shrink-0 w-6 h-6 rounded-full bg-gray-500 text-white flex items-center justify-center text-sm font-bold">A</span>
                    <p class="text-gray-700 dark:text-gray-300 leading-relaxed whitespace-pre-wrap">{selectedFaq.answer}</p>
                </div>
            </div>

            <!-- 메타 정보 -->
            <div class="grid grid-cols-2 gap-4 pt-4 border-t border-gray-100 dark:border-gray-800">
                <div>
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록인</p>
                    <div class="flex items-center gap-2">
                        <div class="w-8 h-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 text-sm font-medium">
                            {selectedFaq.authorName?.charAt(0) || '?'}
                        </div>
                        <div>
                            <p class="text-sm text-gray-800 dark:text-gray-200">{selectedFaq.authorName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{selectedFaq.authorEmail}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">등록일</p>
                    <p class="text-sm text-gray-800 dark:text-gray-200">{selectedFaq.createdDate}</p>
                    {#if selectedFaq.updatedDate && selectedFaq.updatedDate !== selectedFaq.createdDate}
                        <p class="text-xs text-gray-500 dark:text-gray-400 mt-1">
                            수정: {selectedFaq.updatedDate}
                        </p>
                    {/if}
                </div>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <div class="flex items-center justify-between w-full">
            <button
                onclick={handleDeleteFromModal}
                class="px-4 py-2 text-sm font-medium text-red-600 dark:text-red-400 hover:bg-red-50 dark:hover:bg-red-900/30 rounded-lg transition-colors flex items-center gap-2"
            >
                <Icon name="Trash2" size="sm" />
                삭제
            </button>
            <div class="flex items-center gap-2">
                <button
                    onclick={() => showDetailModal = false}
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    닫기
                </button>
                <button
                    onclick={handleEditFromModal}
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-2"
                >
                    <Icon name="Pencil" size="sm" />
                    수정하기
                </button>
            </div>
        </div>
    {/snippet}
</Modal>

<!-- 삭제 확인 모달 -->
{#if showDeleteConfirm}
    <div class="fixed inset-0 z-[60] flex items-center justify-center">
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
                    <h3 class="text-lg font-medium text-gray-900 dark:text-white">FAQ 삭제</h3>
                    <p class="text-sm text-gray-500 dark:text-gray-400">이 작업은 되돌릴 수 없습니다.</p>
                </div>
            </div>
            
            <p class="text-sm text-gray-600 dark:text-gray-300 mb-6">
                "<span class="font-medium">{faqToDelete?.question}</span>" FAQ를 삭제하시겠습니까?
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