<!-- /routes/(apps)/rms-admin/InquiryTable.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    /**
     * @typedef {Object} Inquiry
     * @property {string|number} inquiryId - 문의 ID
     * @property {string} inquiryDate - 문의일
     * @property {string} inquirerName - 문의자 이름
     * @property {string} inquirerEmail - 문의자 이메일
     * @property {string} [inquirerTeam] - 문의자 소속
     * @property {string} [inquirerPhone] - 문의자 전화번호
     * @property {string} title - 문의 제목
     * @property {string} content - 문의 내용
     * @property {string} status - 상태 (pending, answered)
     * @property {string|null} responderName - 답변자 이름
     * @property {string|null} [responderEmail] - 답변자 이메일
     * @property {string|null} [respondedDate] - 답변일
     * @property {string|null} [response] - 답변 내용
     */

    let {
        data = [],
        showTeam = true,
        onRowClick = () => {},
        emptyMessage = '문의 내역이 없습니다.',
        titleMaxWidth = '250px',
    } = $props();

    // 상태별 스타일
    const statusConfig = {
        answered: {
            label: '답변완료',
            class: 'bg-success-100 text-success-700 dark:bg-success-900 dark:text-success-400'
        },
        pending: {
            label: '답변대기',
            class: 'bg-warning-100 text-warning-700 dark:bg-warning-900 dark:text-warning-400'
        }
    };

    function getStatusConfig(status) {
        return statusConfig[status] || statusConfig.pending;
    }

    function handleRowClick(inquiry) {
        onRowClick(inquiry);
    }
</script>

{#if data.length > 0}
    <table class="w-full">
        <thead>
            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">문의 제목</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">문의일</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">문의자</th>
                {#if showTeam}
                    <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">소속</th>
                {/if}
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">답변자</th>
                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
            </tr>
        </thead>
        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each data as inquiry}
                <tr
                    class="hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors cursor-pointer"
                    onclick={() => handleRowClick(inquiry)}
                >
                    <td class="px-4 py-3">
                        <div class="flex items-center gap-3">
                            <div class="w-8 h-8 rounded-lg bg-blue-100 dark:bg-blue-900 flex items-center justify-center text-blue-600 dark:text-blue-400 flex-shrink-0">
                                <Icon name="MessageSquare" size="sm"/>
                            </div>
                            <span class="text-sm text-gray-800 dark:text-gray-200 truncate" style="max-width: {titleMaxWidth}">
                                {inquiry.title}
                            </span>
                        </div>
                    </td>
                    <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                        {inquiry.inquiryDate}
                    </td>
                    <td class="px-4 py-3">
                        <div class="text-left">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{inquiry.inquirerName}</p>
                            <p class="text-xs text-gray-500 dark:text-gray-400">{inquiry.inquirerEmail}</p>
                        </div>
                    </td>
                    {#if showTeam}
                        <td class="px-4 py-3">
                            <p class="text-sm text-gray-800 dark:text-gray-200">{inquiry.inquirerTeam || '-'}</p>
                        </td>
                    {/if}
                    <td class="px-4 py-3">
                        <span class="px-2 py-1 text-xs font-medium {getStatusConfig(inquiry.status).class} rounded-full">
                            {getStatusConfig(inquiry.status).label}
                        </span>
                    </td>
                    <td class="px-4 py-3">
                        {#if inquiry.responderName}
                            <div class="text-left">
                                <p class="text-sm text-gray-800 dark:text-gray-200">{inquiry.responderName}</p>
                                <p class="text-xs text-gray-500 dark:text-gray-400">{inquiry.responderEmail || ''}</p>
                            </div>
                        {:else}
                            <span class="text-sm text-gray-400 dark:text-gray-500">-</span>
                        {/if}
                    </td>
                    <td class="px-4 py-3">
                        <a
                            href="/rms-admin/inquiries/{inquiry.inquiryId}"
                            class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors inline-flex"
                            onclick={(e) => e.stopPropagation()}
                        >
                            <Icon name="ChevronRight" size="sm"/>
                        </a>
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
{:else}
    <div class="text-center py-12">
        <div class="w-16 h-16 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
            <Icon name="MessageSquare" size="lg" />
        </div>
        <p class="text-gray-500 dark:text-gray-400">{emptyMessage}</p>
    </div>
{/if}