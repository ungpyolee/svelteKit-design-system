<script>
    import Icon from "$lib/icons/icon.svelte";
    
    let currentTeamId = $state(1); // 현재 선택된 팀
    
    let teams = $state([
        { id: 1, name: 'CLEW', domain: 'clew.tech' },
        { id: 2, name: 'Motorspace', domain: 'clew.tech' },
        { id: 3, name: 'GENEERS', domain: 'clew.tech' },
    ]);
    
    function switchTeam(teamId) {
        currentTeamId = teamId;
        // TODO: API 호출 또는 store 업데이트
    }
</script>

<div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
    <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
        <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Switch Team</p>
        <a href="/settings/teams" class="text-sm text-primary hover:text-primary-700">
            Manage teams
        </a>
    </header>
    <section class="p-2">
        <ul class="divide-y divide-gray-100 dark:divide-gray-800">
            {#each teams as team}
                <li>
                    <button 
                        onclick={() => switchTeam(team.id)}
                        class="w-full flex items-center justify-between px-4 py-3 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors"
                    >
                        <div class="flex items-center gap-3">
                            <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold">
                                {team.name.charAt(0)}
                            </div>
                            <div class="text-left">
                                <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{team.name}</p>
                                <p class="text-xs text-gray-500 dark:text-gray-400">{team.domain}</p>
                            </div>
                        </div>
                        
                        {#if currentTeamId === team.id}
                            <span class="flex items-center gap-1.5 px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">
                                <Icon name="Check" size="xs" />
                                현재 팀
                            </span>
                        {:else}
                            <span class="text-xs text-gray-400 group-hover:text-primary">
                                팀 전환
                            </span>
                        {/if}
                    </button>
                </li>
            {/each}
        </ul>
    </section>
</div>