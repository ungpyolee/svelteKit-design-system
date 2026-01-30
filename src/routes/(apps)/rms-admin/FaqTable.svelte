<!-- /routes/(apps)/rms-admin/FaqTable.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    /**
     * @typedef {Object} Faq
     * @property {string|number} faqId - FAQ ID
     * @property {string} createdDate - 등록일
     * @property {string} question - 질문
     * @property {string} answer - 답변
     * @property {boolean} isPublished - 게시 상태
     * @property {string} authorName - 등록인 이름
     * @property {string} authorEmail - 등록인 이메일
     * @property {string} [updatedDate] - 수정일
     */

    let {
        data = [],
        onEdit = () => {},
        onDelete = () => {},
        onTogglePublish = () => {},
        emptyMessage = '등록된 FAQ가 없습니다.',
        questionMaxWidth = '300px',
    } = $props();

    // 삭제 확인 모달 상태
    let showDeleteConfirm = $state(false);
    let selectedFaq = $state(null);

    function handleEditClick(faq, e) {
        e.stopPropagation();
        onEdit(faq);
    }

    function handleDeleteClick(faq, e) {
        e.stopPropagation();
        selectedFaq = faq;
        showDeleteConfirm = true;
    }

    function confirmDelete() {
        if (selectedFaq) {
            onDelete(selectedFaq);
        }
        showDeleteConfirm = false;
        selectedFaq = null;
    }

    function handleTogglePublish(faq, e) {
        e.stopPropagation();
        onTogglePublish(faq);
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
                <tr class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors">
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
                    <h3 class="text-lg font-medium text-gray-900 dark:text-white">FAQ 삭제</h3>
                    <p class="text-sm text-gray-500 dark:text-gray-400">이 작업은 되돌릴 수 없습니다.</p>
                </div>
            </div>
            
            <p class="text-sm text-gray-600 dark:text-gray-300 mb-6">
                "<span class="font-medium">{selectedFaq?.question}</span>" FAQ를 삭제하시겠습니까?
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