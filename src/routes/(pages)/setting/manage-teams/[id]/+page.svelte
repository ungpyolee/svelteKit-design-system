<script>
    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";

    // URL에서 팀 ID 가져오기
    let teamId = $derived($page.params.id);

    // ========== 팀 정보 (더미 데이터) ==========
    let team = $state({
        id: 'team_001',
        name: 'CLEW',
        slug: 'CLEW',
        description: 'Main development team for CLEW platform',
        createdAt: '2024-03-15',
        isActive: true
    });

    // ========== 멤버 목록 (더미 데이터) ==========
    let members = $state([
        {
            id: 'member_001',
            name: '김철수',
            email: 'cs.kim@clew.tech',
            role: 'Owner',
            registeredAt: '2024-03-15'
        },
        {
            id: 'member_002',
            name: '박영희',
            email: 'yh.park@clew.tech',
            role: 'Member',
            registeredAt: '2024-03-20'
        },
        {
            id: 'member_003',
            name: '이민수',
            email: 'ms.lee@clew.tech',
            role: 'Member',
            registeredAt: '2024-04-05'
        },
        {
            id: 'member_004',
            name: '정다운',
            email: 'dw.jung@clew.tech',
            role: 'Member',
            registeredAt: '2024-05-12'
        },
        {
            id: 'member_005',
            name: '최지훈',
            email: 'jh.choi@clew.tech',
            role: 'Member',
            registeredAt: '2024-06-01'
        },
    ]);

    // Role 옵션
    const roleOptions = ['Owner', 'Member'];

    // Role 스타일 config
    const roleConfig = {
        'Owner': { class: 'bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400' },
        'Member': { class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300' },
    };

    // 모달 상태
    let showEditTeamModal = $state(false);
    let showAddMemberModal = $state(false);
    let showDeleteConfirmModal = $state(false);
    let selectedMember = $state(null);

    // 팀 수정 폼 데이터
    let editTeamForm = $state({
        name: '',
        slug: '',
        description: ''
    });

    // 멤버 추가 폼 데이터
    let addMemberForm = $state({
        name: '',
        email: '',
        role: 'Member'
    });

    // 페이지네이션
    let currentPage = $state(1);
    const totalItems = 5;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }

    // 팀 수정 모달 열기
    function openEditTeamModal() {
        editTeamForm = {
            name: team.name,
            slug: team.slug,
            description: team.description
        };
        showEditTeamModal = true;
    }

    // 팀 정보 저장
    function handleSaveTeam() {
        team.name = editTeamForm.name;
        team.slug = editTeamForm.slug;
        team.description = editTeamForm.description;
        showEditTeamModal = false;
        console.log('Team updated:', team);
    }

    // 멤버 추가 모달 열기
    function openAddMemberModal() {
        addMemberForm = {
            name: '',
            email: '',
            role: 'Member'
        };
        showAddMemberModal = true;
    }

    // 멤버 추가
    function handleAddMember() {
        const newMember = {
            id: `member_${Date.now()}`,
            name: addMemberForm.name,
            email: addMemberForm.email,
            role: addMemberForm.role,
            registeredAt: new Date().toISOString().split('T')[0]
        };
        members = [...members, newMember];
        showAddMemberModal = false;
        console.log('Member added:', newMember);
    }

    // 권한 변경
    function handleRoleChange(member, newRole) {
        member.role = newRole;
        console.log('Role changed:', member.id, newRole);
        // TODO: API 호출
    }

    // 멤버 삭제 확인 모달 열기
    function openDeleteConfirmModal(member) {
        selectedMember = member;
        showDeleteConfirmModal = true;
    }

    // 멤버 삭제
    function handleDeleteMember() {
        members = members.filter(m => m.id !== selectedMember.id);
        showDeleteConfirmModal = false;
        selectedMember = null;
        console.log('Member deleted');
    }
</script>

<div class="space-y-6">
    <!-- 뒤로가기 & 타이틀 -->
    <div class="flex items-center gap-4">
        <a
            href="/setting/manage-teams"
            class="p-2 text-gray-500 hover:text-gray-700 dark:text-gray-400 dark:hover:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            <Icon name="ArrowLeft" size="sm" />
        </a>
        <div>
            <h1 class="text-xl font-semibold text-gray-900 dark:text-white">{team.name}</h1>
            <p class="text-sm text-gray-500 dark:text-gray-400">팀 정보 및 멤버 관리</p>
        </div>
    </div>

    <!-- 팀 정보 카드 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">팀 정보</p>
            <button
                onclick={openEditTeamModal}
                class="px-4 py-2 text-sm font-medium text-primary border border-primary rounded-lg hover:bg-primary hover:text-white transition-colors flex items-center gap-1.5"
            >
                <Icon name="Pencil" size="sm" />
                팀 정보 수정
            </button>
        </header>
        <section class="p-6">
            <div class="flex items-start gap-6">
                <!-- 팀 아이콘 -->
                <div class="w-16 h-16 rounded-xl bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-bold text-2xl flex-shrink-0">
                    {team.name.charAt(0)}
                </div>

                <!-- 팀 상세 정보 -->
                <div class="flex-1 space-y-4">
                    <div>
                        <h2 class="text-xl font-semibold text-gray-900 dark:text-white">{team.name}</h2>
                        <p class="text-sm text-gray-500 dark:text-gray-400 mt-1">{team.description}</p>
                    </div>

                    <div class="flex items-center gap-6 text-sm">
                        <div class="flex items-center gap-2">
                            <span class="text-gray-500 dark:text-gray-400">Slug:</span>
                            <code class="px-2 py-0.5 bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300 rounded font-mono text-xs">{team.slug}</code>
                        </div>
                        <div class="flex items-center gap-2">
                            <span class="text-gray-500 dark:text-gray-400">ID:</span>
                            <code class="px-2 py-0.5 bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300 rounded font-mono text-xs">{team.id}</code>
                        </div>
                        <div class="flex items-center gap-2">
                            <span class="text-gray-500 dark:text-gray-400">생성일:</span>
                            <span class="text-gray-700 dark:text-gray-300">{formatDate(team.createdAt)}</span>
                        </div>
                        <div class="flex items-center gap-2">
                            <span class="text-gray-500 dark:text-gray-400">상태:</span>
                            {#if team.isActive}
                                <span class="inline-flex items-center gap-1.5 px-2 py-0.5 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">
                                    <span class="w-1.5 h-1.5 bg-success-500 rounded-full"></span>
                                    Active
                                </span>
                            {:else}
                                <span class="inline-flex items-center gap-1.5 px-2 py-0.5 text-xs font-medium bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400 rounded-full">
                                    <span class="w-1.5 h-1.5 bg-gray-400 rounded-full"></span>
                                    Disabled
                                </span>
                            {/if}
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- 멤버 목록 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <div class="flex items-center gap-3">
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">멤버 목록</p>
                <span class="px-2 py-0.5 text-xs font-medium bg-gray-100 dark:bg-gray-800 text-gray-600 dark:text-gray-400 rounded-full">
                    {members.length}명
                </span>
            </div>
            <button
                onclick={openAddMemberModal}
                class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-1.5"
            >
                <Icon name="UserPlus" size="sm" />
                멤버 등록
            </button>
        </header>
        <section class="p-6">
            {#if members.length > 0}
                <table class="w-full">
                    <thead>
                        <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">이름</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">이메일</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">권한</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">등록일</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                        {#each members as member}
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <!-- 이름 -->
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-3">
                                        <div class="w-8 h-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium text-sm">
                                            {member.name.charAt(0)}
                                        </div>
                                        <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{member.name}</span>
                                    </div>
                                </td>

                                <!-- 이메일 -->
                                <td class="px-4 py-3">
                                    <span class="text-sm text-gray-600 dark:text-gray-400">{member.email}</span>
                                </td>

                                <!-- 권한 -->
                                <td class="px-4 py-3">
                                    <select
                                        value={member.role}
                                        onchange={(e) => handleRoleChange(member, e.target.value)}
                                        class="px-2 py-1 text-xs font-medium rounded-lg border-0 cursor-pointer focus:ring-2 focus:ring-primary/20 {roleConfig[member.role]?.class}"
                                    >
                                        {#each roleOptions as role}
                                            <option value={role} class="bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200">
                                                {role}
                                            </option>
                                        {/each}
                                    </select>
                                </td>

                                <!-- 등록일 -->
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                    {formatDate(member.registeredAt)}
                                </td>

                                <!-- 액션 -->
                                <td class="px-4 py-3">
                                    <button
                                        onclick={() => openDeleteConfirmModal(member)}
                                        class="p-1.5 text-gray-400 hover:text-danger hover:bg-danger-50 dark:hover:bg-danger-900/20 rounded-lg transition-colors"
                                        title="멤버 삭제"
                                    >
                                        <Icon name="Trash2" size="sm" />
                                    </button>
                                </td>
                            </tr>
                        {/each}
                    </tbody>
                </table>
            {:else}
                <div class="text-center py-12">
                    <div class="w-16 h-16 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
                        <Icon name="Users" size="lg" />
                    </div>
                    <p class="text-gray-500 dark:text-gray-400">등록된 멤버가 없습니다.</p>
                    <button
                        onclick={openAddMemberModal}
                        class="mt-4 px-4 py-2 text-sm font-medium text-primary hover:bg-primary-50 dark:hover:bg-primary-900/20 rounded-lg transition-colors"
                    >
                        첫 번째 멤버 등록하기
                    </button>
                </div>
            {/if}
        </section>
        {#if members.length > itemsPerPage}
            <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
                <Pagination
                    bind:currentPage={currentPage}
                    {totalPages}
                    {totalItems}
                    {itemsPerPage}
                />
            </footer>
        {/if}
    </div>
</div>


<!-- 팀 정보 수정 모달 -->
<Modal bind:isOpen={showEditTeamModal} title="팀 정보 수정" size="md">
    <form class="space-y-4" onsubmit={(e) => { e.preventDefault(); handleSaveTeam(); }}>
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                팀 이름 <span class="text-danger">*</span>
            </label>
            <input
                type="text"
                bind:value={editTeamForm.name}
                placeholder="팀 이름을 입력하세요"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                required
            />
        </div>

        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                Slug <span class="text-danger">*</span>
            </label>
            <input
                type="text"
                bind:value={editTeamForm.slug}
                placeholder="URL에 사용될 slug"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors font-mono"
                required
            />
        </div>

        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                설명
            </label>
            <textarea
                bind:value={editTeamForm.description}
                rows="3"
                placeholder="팀에 대한 설명을 입력하세요"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors resize-none"
            ></textarea>
        </div>
    </form>

    {#snippet footer()}
        <button
            onclick={() => showEditTeamModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button
            onclick={handleSaveTeam}
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            저장
        </button>
    {/snippet}
</Modal>


<!-- 멤버 등록 모달 -->
<Modal bind:isOpen={showAddMemberModal} title="멤버 등록" size="md">
    <form class="space-y-4" onsubmit={(e) => { e.preventDefault(); handleAddMember(); }}>
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                이름 <span class="text-danger">*</span>
            </label>
            <input
                type="text"
                bind:value={addMemberForm.name}
                placeholder="멤버 이름을 입력하세요"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                required
            />
        </div>

        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                이메일 <span class="text-danger">*</span>
            </label>
            <input
                type="email"
                bind:value={addMemberForm.email}
                placeholder="이메일 주소를 입력하세요"
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                required
            />
        </div>

        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                권한 <span class="text-danger">*</span>
            </label>
            <select
                bind:value={addMemberForm.role}
                class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
            >
                {#each roleOptions as role}
                    <option value={role}>{role}</option>
                {/each}
            </select>
        </div>
    </form>

    {#snippet footer()}
        <button
            onclick={() => showAddMemberModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button
            onclick={handleAddMember}
            disabled={!addMemberForm.name || !addMemberForm.email}
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
        >
            등록
        </button>
    {/snippet}
</Modal>


<!-- 멤버 삭제 확인 모달 -->
<Modal bind:isOpen={showDeleteConfirmModal} title="멤버 삭제" size="sm">
    {#if selectedMember}
        <div class="text-center py-4">
            <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-danger-100 dark:bg-danger-900/30 flex items-center justify-center text-danger">
                <Icon name="AlertTriangle" size="md" />
            </div>
            <p class="text-gray-800 dark:text-gray-200 mb-2">
                <strong>{selectedMember.name}</strong> 님을 팀에서 삭제하시겠습니까?
            </p>
            <p class="text-sm text-gray-500 dark:text-gray-400">
                이 작업은 되돌릴 수 없습니다.
            </p>
        </div>
    {/if}

    {#snippet footer()}
        <button
            onclick={() => showDeleteConfirmModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button
            onclick={handleDeleteMember}
            class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
        >
            삭제
        </button>
    {/snippet}
</Modal>
