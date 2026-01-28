<script>
    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";
    
    let teams = $state([
        {
            id: 'team_001',
            name: 'CLEW',
            slug: 'CLEW',
            description: 'Main development team for CLEW platform',
            role: 'Owner',
            createdAt: '2024-03-15',
            isActive: true
        },
        {
            id: 'team_002',
            name: 'Motorspace',
            slug: 'Motorspace',
            description: 'Electric motor design and simulation team',
            role: 'Member',
            createdAt: '2024-05-22',
            isActive: true
        },
        {
            id: 'team_003',
            name: 'GENEERS',
            slug: 'GENEERS',
            description: 'Engineering solutions and consulting',
            role: 'Member',
            createdAt: '2024-08-10',
            isActive: true
        },
        {
            id: 'team_004',
            name: 'Legacy Project',
            slug: 'Legacy Project',
            description: 'Archived project team',
            role: 'Member',
            createdAt: '2023-11-05',
            isActive: false
        },
    ]);

    // Role 스타일 config
    const roleConfig = {
        'Owner': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        'Member': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
    };
    
    // 모달 상태
    let showDetailModal = $state(false);
    let showCreateModal = $state(false);
    let selectedTeam = $state(null);
    
    // 페이지네이션
    let currentPage = $state(1);
    const totalItems = 4;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);
    
    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }
    
    function handleRowClick(team) {
        selectedTeam = team;
        showDetailModal = true;
    }
    
    function handleEdit(e, team) {
        e.stopPropagation();
        console.log('Edit team:', team.id);
        // TODO: 수정 모달 또는 페이지로 이동
    }
    
    function handleToggleStatus(e, team) {
        e.stopPropagation();
        const action = team.isActive ? 'disable' : 'enable';
        if (confirm(`Are you sure you want to ${action} "${team.name}"?`)) {
            team.isActive = !team.isActive;
        }
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <!-- Teams Table -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">팀/조직 관리</p>
            <div class="flex items-center gap-2">
                <button 
                    onclick={() => showCreateModal = true}
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center"
                >
                    <Icon name="Plus" size="sm" class="inline mr-1" />
                    팀/조직 생성하기
                </button>
            </div>
        </header>
        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">팀/조직</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Slug</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">내 권한</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">생성일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-32"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each teams as team}
                        <tr 
                            class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer {!team.isActive ? 'opacity-60' : ''}"
                            onclick={() => handleRowClick(team)}
                        >
                            <!-- Team Info -->
                            <td class="px-4 py-3">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold">
                                        {team.name.charAt(0)}
                                    </div>
                                    <div>
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{team.name}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400 truncate max-w-[200px]">{team.description}</p>
                                    </div>
                                </div>
                            </td>
                            
                            <!-- Team Slug -->
                            <td class="px-4 py-3">
                                <code class="text-xs font-mono text-gray-500 dark:text-gray-400 bg-gray-100 dark:bg-gray-800 px-2 py-1 rounded">
                                    {team.slug}
                                </code>
                            </td>
                            
                            <!-- Role -->
                            <td class="px-4 py-3">
                                <span class="px-2 py-1 text-xs font-medium {roleConfig[team.role]?.class} rounded-full">
                                    {team.role}
                                </span>
                            </td>
                            
                            <!-- Created At -->
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(team.createdAt)}
                            </td>
                            
                            <!-- Status -->
                            <td class="px-4 py-3">
                                {#if team.isActive}
                                    <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">
                                        <span class="w-1.5 h-1.5 bg-success-500 rounded-full"></span>
                                        Active
                                    </span>
                                {:else}
                                    <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400 rounded-full">
                                        <span class="w-1.5 h-1.5 bg-gray-400 rounded-full"></span>
                                        Disabled
                                    </span>
                                {/if}
                            </td>
                            
                            <!-- Actions -->
                            <td class="px-4 py-3">
                                <div class="flex items-center gap-1">
                                    {#if team.role === 'Owner'}
                                        <button
                                            onclick={(e) => handleEdit(e, team)}
                                            class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                            title="Edit"
                                        >
                                            <Icon name="Pencil" size="sm" />
                                        </button>
                                        <button
                                            onclick={(e) => handleToggleStatus(e, team)}
                                            class="p-1.5 text-gray-400 hover:text-warning-600 hover:bg-warning-50 dark:hover:bg-warning-900/20 rounded-lg transition-colors"
                                            title={team.isActive ? 'Disable' : 'Enable'}
                                        >
                                            {#if team.isActive}
                                                <Icon name="Ban" size="sm" />
                                            {:else}
                                                <Icon name="RotateCcw" size="sm" />
                                            {/if}
                                        </button>
                                    {/if}
                                    <button 
                                        class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                        onclick={(e) => { e.stopPropagation(); handleRowClick(team); }}
                                    >
                                        <Icon name="ChevronRight" size="sm"/>
                                    </button>
                                </div>
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>
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


<!-- Team Detail Modal -->
<Modal bind:isOpen={showDetailModal} title="팀 정보" size="lg">
    {#if selectedTeam}
        <div class="space-y-6">
            <!-- Team 기본 정보 -->
            <div class="flex items-start gap-4">
                <div class="w-12 h-12 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold text-lg">
                    {selectedTeam.name.charAt(0)}
                </div>
                <div class="flex-1">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedTeam.name}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">{selectedTeam.description}</p>
                </div>
            </div>

            <!-- 상태 & Role 뱃지 -->
            <div class="flex items-center gap-2">
                <span class="px-2 py-1 text-xs font-medium {roleConfig[selectedTeam.role]?.class} rounded-full">
                    {selectedTeam.role}
                </span>
                {#if selectedTeam.isActive}
                    <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">
                        <span class="w-1.5 h-1.5 bg-success-500 rounded-full"></span>
                        Active
                    </span>
                {:else}
                    <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400 rounded-full">
                        <span class="w-1.5 h-1.5 bg-gray-400 rounded-full"></span>
                        Disabled
                    </span>
                {/if}
            </div>

            <!-- 상세 정보 그리드 -->
            <div class="grid grid-cols-3 gap-4">
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">Slug</p>
                    <code class="text-sm font-mono font-medium text-gray-800 dark:text-gray-200">{selectedTeam.slug}</code>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">Team ID</p>
                    <code class="text-sm font-mono font-medium text-gray-800 dark:text-gray-200">{selectedTeam.id}</code>
                </div>
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">생성일</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{formatDate(selectedTeam.createdAt)}</p>
                </div>
            </div>

            <!-- 권한 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">당신의 권한</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center">
                        <Icon name="Shield" size="sm" class="text-gray-600 dark:text-gray-300" />
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedTeam.role}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">
                            {#if selectedTeam.role === 'Owner'}
                                Full access to all team settings and members
                            {:else}
                                Can view team resources and collaborate
                            {/if}
                        </p>
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
        <a
            href="/setting/manage-teams/{selectedTeam?.id}"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            멤버 관리
        </a>
    {/snippet}
</Modal>


<!-- Create Team Modal -->
<Modal bind:isOpen={showCreateModal} title="팀/조직 생성" size="md">
    <form class="space-y-4">
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                팀/조직 이름
            </label>
            <input 
                type="text"
                placeholder="Enter team name"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
            />
        </div>
        
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                설명
            </label>
            <textarea 
                rows="3"
                placeholder="Enter team description"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
            ></textarea>
        </div>
    </form>

    {#snippet footer()}
        <button 
            onclick={() => showCreateModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button 
            type="submit"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            생성하기
        </button>
    {/snippet}
</Modal>