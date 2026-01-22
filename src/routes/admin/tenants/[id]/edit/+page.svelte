<script>
    /**
     * 테넌트 관리 - 수정 페이지
     * 테넌트 정보 수정 + 멤버 목록 조회(읽기 전용)
     */
    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    let tenantId = $derived($page.params.id);

    let formData = $state({
        name: 'CLEW',
        slug: 'clew',
        status: 'Active',
    });

    // 멤버 목록 (읽기 전용)
    let members = $state([
        {
            userId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            displayName: '김철수',
            email: 'cs.kim@company.com',
            role: 'admin',
            status: 'active',
            joinedAt: '2024-01-15T09:00:00.000Z',
        },
        {
            userId: '4fa85f64-5717-4562-b3fc-2c963f66afa7',
            displayName: '이민수',
            email: 'ms.lee@company.com',
            role: 'member',
            status: 'active',
            joinedAt: '2024-02-20T10:30:00.000Z',
        },
        {
            userId: '5fa85f64-5717-4562-b3fc-2c963f66afa8',
            displayName: '박지영',
            email: 'jy.park@company.com',
            role: 'member',
            status: 'suspended',
            joinedAt: '2024-03-10T14:20:00.000Z',
        },
    ]);

    const statusOptions = [
        { value: 'Active', label: 'Active' },
        { value: 'Suspended', label: 'Suspended' },
        { value: 'Inactive', label: 'Inactive' },
    ];

    const roleConfig = {
        'admin': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        'member': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
    };

    const memberStatusConfig = {
        'active': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400', dot: 'bg-success-500' },
        'suspended': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400', dot: 'bg-warning-500' },
    };

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }

    function handleSubmit(e) {
        e.preventDefault();
        console.log('Update tenant:', tenantId, formData);
    }

    function handleDelete() {
        if (confirm('정말 이 테넌트를 삭제하시겠습니까?')) {
            console.log('Delete tenant:', tenantId);
        }
    }
</script>

<div class="max-w-4xl mx-auto space-y-6">
    <!-- 테넌트 정보 수정 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/admin/tenants"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">테넌트 수정</p>
                    <code class="text-sm text-gray-500 dark:text-gray-400 font-mono">{formData.slug}</code>
                </div>
            </div>
        </header>

        <form onsubmit={handleSubmit} class="p-6 space-y-6">
            <div class="space-y-4">
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            테넌트 이름 <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="text"
                            bind:value={formData.name}
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            Slug <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="text"
                            bind:value={formData.slug}
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors font-mono"
                        />
                    </div>
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        상태
                    </label>
                    <select
                        bind:value={formData.status}
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    >
                        {#each statusOptions as option}
                            <option value={option.value}>{option.label}</option>
                        {/each}
                    </select>
                </div>
            </div>

            <div class="flex items-center justify-between pt-4 border-t border-gray-100 dark:border-gray-800">
                <button
                    type="button"
                    onclick={handleDelete}
                    class="px-4 py-2 text-sm font-medium text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                >
                    테넌트 삭제
                </button>
                <div class="flex items-center gap-3">
                    <a
                        href="/admin/tenants"
                        class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        취소
                    </a>
                    <button
                        type="submit"
                        class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                    >
                        저장
                    </button>
                </div>
            </div>
        </form>
    </div>

    <!-- 멤버 목록 (읽기 전용) -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <div>
                <p class="text-sm font-semibold text-gray-800 dark:text-gray-100">멤버 목록</p>
                <p class="text-xs text-gray-500 dark:text-gray-400">읽기 전용 - 멤버 관리는 테넌트 관리자가 수행합니다</p>
            </div>
            <span class="text-sm text-gray-500 dark:text-gray-400">{members.length}명</span>
        </header>

        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">멤버</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">역할</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">가입일</th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each members as member}
                        <tr>
                            <td class="px-4 py-3">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-600 dark:text-gray-400 font-medium">
                                        {member.displayName.charAt(0)}
                                    </div>
                                    <div>
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{member.displayName}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400">{member.email}</p>
                                    </div>
                                </div>
                            </td>
                            <td class="px-4 py-3">
                                <span class="px-2 py-1 text-xs font-medium {roleConfig[member.role]?.class} rounded-full">
                                    {member.role}
                                </span>
                            </td>
                            <td class="px-4 py-3">
                                <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium {memberStatusConfig[member.status]?.class} rounded-full">
                                    <span class="w-1.5 h-1.5 {memberStatusConfig[member.status]?.dot} rounded-full"></span>
                                    {member.status}
                                </span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(member.joinedAt)}
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>
    </div>
</div>
