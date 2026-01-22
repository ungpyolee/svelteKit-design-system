<script>
    /**
     * 사용자 상세 모달 컴포넌트
     */
    import Icon from "$lib/icons/icon.svelte";
    import { Modal } from "$lib/components/ui";

    let { isOpen = $bindable(false), user = null } = $props();

    const statusConfig = {
        'Active': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400', dot: 'bg-success-500' },
        'Inactive': { class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400', dot: 'bg-gray-400' },
        'Pending': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400', dot: 'bg-warning-500' },
    };

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }
</script>

<Modal bind:isOpen={isOpen} title="사용자 정보" size="lg">
    {#if user}
        <div class="space-y-6">
            <!-- 사용자 기본 정보 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-full bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold text-lg">
                    {user.displayName.charAt(0)}
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{user.displayName}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">{user.email}</p>
                </div>
                <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium {statusConfig[user.status]?.class} rounded-full">
                    <span class="w-1.5 h-1.5 {statusConfig[user.status]?.dot} rounded-full"></span>
                    {user.status}
                </span>
            </div>

            <!-- 상세 정보 그리드 -->
            <div class="grid grid-cols-2 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">소속</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{user.affiliation}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">연락처</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{user.phone}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">가입일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{formatDate(user.createdAt)}</p>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">External ID</p>
                    <code class="text-xs font-mono text-gray-600 dark:text-gray-300">{user.externalId}</code>
                </div>
            </div>

            <!-- User ID -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-2">User ID</p>
                <code class="text-xs font-mono text-gray-600 dark:text-gray-300 bg-gray-100 dark:bg-gray-800 px-2 py-1 rounded">
                    {user.userId}
                </code>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <button
            onclick={() => isOpen = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            닫기
        </button>
        <a
            href="/admin/users/{user?.userId}/edit"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            수정
        </a>
    {/snippet}
</Modal>
