<script>
    /**
     * CMS - Space 수정 페이지
     * Space 정보 수정 + 멤버 관리
     */
    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    let spaceId = $derived($page.params.id);

    let formData = $state({
        name: 'Tech Blog',
        description: '기술 블로그 - 개발 관련 아티클',
        type: 'Open',
    });

    // Space 멤버 목록
    let members = $state([
        {
            userId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            displayName: '김철수',
            email: 'cs.kim@company.com',
            role: 'Administrator',
            joinedAt: '2024-01-15T09:00:00.000Z',
        },
        {
            userId: '4fa85f64-5717-4562-b3fc-2c963f66afa7',
            displayName: '이민수',
            email: 'ms.lee@company.com',
            role: 'Editor',
            joinedAt: '2024-02-20T10:30:00.000Z',
        },
        {
            userId: '5fa85f64-5717-4562-b3fc-2c963f66afa8',
            displayName: '박지영',
            email: 'jy.park@company.com',
            role: 'Member',
            joinedAt: '2024-03-10T14:20:00.000Z',
        },
    ]);

    const typeOptions = [
        { value: 'Open', label: 'Open', desc: '모든 인증 사용자가 작성/발행 가능' },
        { value: 'Moderated', label: 'Moderated', desc: '모든 사용자 작성 가능, Editor만 발행' },
        { value: 'Closed', label: 'Closed', desc: 'Space 멤버만 작성/발행 가능' },
    ];

    const roleConfig = {
        'Administrator': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        'Editor': { class: 'bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400' },
        'Member': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
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
        console.log('Update space:', spaceId, formData);
    }

    function handleDelete() {
        if (confirm('정말 이 Space를 삭제하시겠습니까? 모든 콘텐츠가 삭제됩니다.')) {
            console.log('Delete space:', spaceId);
        }
    }
</script>

<div class="max-w-4xl mx-auto space-y-6">
    <!-- Space 정보 수정 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <a
                    href="/apps/cms/spaces"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Space 수정</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">{formData.name}</p>
                </div>
            </div>
        </header>

        <form onsubmit={handleSubmit} class="p-6 space-y-6">
            <div class="space-y-4">
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            Space 이름 <span class="text-red-500">*</span>
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
                            타입
                        </label>
                        <select
                            bind:value={formData.type}
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        >
                            {#each typeOptions as option}
                                <option value={option.value}>{option.label} - {option.desc}</option>
                            {/each}
                        </select>
                    </div>
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        설명
                    </label>
                    <textarea
                        bind:value={formData.description}
                        rows="3"
                        class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
                    ></textarea>
                </div>
            </div>

            <div class="flex items-center justify-between pt-4 border-t border-gray-100 dark:border-gray-800">
                <button
                    type="button"
                    onclick={handleDelete}
                    class="px-4 py-2 text-sm font-medium text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                >
                    Space 삭제
                </button>
                <div class="flex items-center gap-3">
                    <a
                        href="/apps/cms/spaces"
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

    <!-- 멤버 목록 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <div>
                <p class="text-sm font-semibold text-gray-800 dark:text-gray-100">멤버 관리</p>
                <p class="text-xs text-gray-500 dark:text-gray-400">Space 멤버 및 역할 관리</p>
            </div>
            <button class="px-3 py-1.5 text-sm font-medium text-primary border border-primary rounded-lg hover:bg-primary-50 dark:hover:bg-primary-900/20 transition-colors flex items-center gap-1">
                <Icon name="UserPlus" size="sm" />
                멤버 초대
            </button>
        </header>

        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">멤버</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">역할</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">가입일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
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
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(member.joinedAt)}
                            </td>
                            <td class="px-4 py-3">
                                <button class="p-1.5 text-gray-400 hover:text-gray-600 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors">
                                    <Icon name="MoreHorizontal" size="sm"/>
                                </button>
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>
    </div>
</div>
