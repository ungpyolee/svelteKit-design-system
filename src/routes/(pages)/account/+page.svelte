<!-- /routes/(pages)/account/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    // 사용자 정보 (API에서 가져올 데이터)
    let user = $state({
        name: '김철수',
        email: 'cs.kim@company.com',
        phone: '010-1234-5678',
        company: 'CLEW Technology',
        department: '전동기설계팀',
        position: '선임연구원',
        avatar: null,
        bio: '전동기 설계 및 해석 전문가입니다. IPMSM, BLDC 모터 설계 경력 10년.',
        joinedDate: '2024-01-15',
        isContributor: true,
    });

    let isEditing = $state(false);
    let editForm = $state({ ...user });
    let isLoading = $state(false);
    let avatarInput;

    function startEdit() {
        editForm = { ...user };
        isEditing = true;
    }

    function cancelEdit() {
        isEditing = false;
    }

    async function saveProfile() {
        isLoading = true;
        try {
            await new Promise(resolve => setTimeout(resolve, 1000));
            user = { ...editForm };
            isEditing = false;
            console.log('Profile saved:', user);
        } finally {
            isLoading = false;
        }
    }

    function handleAvatarChange(e) {
        const file = e.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                editForm.avatar = e.target?.result;
            };
            reader.readAsDataURL(file);
        }
    }
</script>

<svelte:head>
    <title>내 프로필 - RMS</title>
</svelte:head>

<div class="space-y-6">
    <!-- 프로필 카드 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-200 dark:border-gray-800 rounded-xl overflow-hidden">
        <!-- 커버 -->
        <div class="h-32 bg-gradient-to-r from-primary-500 to-primary-600"></div>
        
        <!-- 프로필 정보 -->
        <div class="p-6">
            <div class="flex flex-col sm:flex-row sm:items-end gap-4 -mt-12">
                <!-- 아바타 -->
                <div class="relative">
                    {#if isEditing}
                        <button
                            onclick={() => avatarInput.click()}
                            class="w-24 h-24 rounded-xl bg-gray-200 dark:bg-gray-700 border-4 border-white dark:border-gray-900 flex items-center justify-center overflow-hidden group"
                        >
                            {#if editForm.avatar}
                                <img src={editForm.avatar} alt="Avatar" class="w-full h-full object-cover" />
                            {:else}
                                <span class="text-3xl font-bold text-gray-400">{user.name?.charAt(0)}</span>
                            {/if}
                            <div class="absolute inset-0 bg-black/50 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity">
                                <Icon name="Camera" size="md" class="text-white" />
                            </div>
                        </button>
                        <input
                            type="file"
                            accept="image/*"
                            bind:this={avatarInput}
                            onchange={handleAvatarChange}
                            class="hidden"
                        />
                    {:else}
                        <div class="w-24 h-24 rounded-xl bg-gray-200 dark:bg-gray-700 border-4 border-white dark:border-gray-900 flex items-center justify-center overflow-hidden">
                            {#if user.avatar}
                                <img src={user.avatar} alt="Avatar" class="w-full h-full object-cover" />
                            {:else}
                                <span class="text-3xl font-bold text-gray-400">{user.name?.charAt(0)}</span>
                            {/if}
                        </div>
                    {/if}
                    {#if user.isContributor}
                        <div class="absolute -bottom-1 -right-1 w-7 h-7 bg-green-500 rounded-lg flex items-center justify-center border-2 border-white dark:border-gray-900">
                            <Icon name="BadgeCheck" size="xs" class="text-white" />
                        </div>
                    {/if}
                </div>

                <!-- 기본 정보 -->
                <div class="flex-1 pt-2 sm:pt-0 mt-4">
                    <div class="flex items-start justify-between">
                        <div>
                            <h1 class="text-xl font-bold text-gray-900 dark:text-white">{user.name}</h1>
                            <p class="text-sm text-gray-500 dark:text-gray-400">{user.email}</p>
                        </div>
                        {#if !isEditing}
                            <button
                                onclick={startEdit}
                                class="px-4 py-2 text-sm font-medium text-primary border border-primary rounded-lg hover:bg-primary-50 dark:hover:bg-primary-900/20 transition-colors flex items-center gap-2"
                            >
                                <Icon name="Pencil" size="sm" />
                                수정
                            </button>
                        {/if}
                    </div>
                </div>
            </div>

            <!-- 뱃지 -->
            <div class="flex flex-wrap gap-2 mt-4">
                {#if user.isContributor}
                    <span class="inline-flex items-center gap-1 px-2.5 py-1 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 text-xs font-medium rounded-full">
                        <Icon name="BadgeCheck" size="xs" />
                        기여자
                    </span>
                {/if}
                <span class="inline-flex items-center gap-1 px-2.5 py-1 bg-gray-100 dark:bg-gray-800 text-gray-600 dark:text-gray-400 text-xs font-medium rounded-full">
                    <Icon name="Calendar" size="xs" />
                    {user.joinedDate} 가입
                </span>
            </div>
        </div>
    </div>

    <!-- 프로필 상세 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-200 dark:border-gray-800 rounded-xl">
        <div class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <h2 class="text-lg font-semibold text-gray-900 dark:text-white">프로필 정보</h2>
        </div>

        {#if isEditing}
            <!-- 수정 폼 -->
            <form onsubmit={(e) => { e.preventDefault(); saveProfile(); }} class="p-6 space-y-6">
                <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">이름</label>
                        <input
                            type="text"
                            bind:value={editForm.name}
                            class="w-full px-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">연락처</label>
                        <input
                            type="tel"
                            bind:value={editForm.phone}
                            class="w-full px-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">회사/소속</label>
                        <input
                            type="text"
                            bind:value={editForm.company}
                            class="w-full px-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">부서</label>
                        <input
                            type="text"
                            bind:value={editForm.department}
                            class="w-full px-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">직책</label>
                        <input
                            type="text"
                            bind:value={editForm.position}
                            class="w-full px-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                        />
                    </div>
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">자기소개</label>
                    <textarea
                        bind:value={editForm.bio}
                        rows="3"
                        class="w-full px-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none resize-none"
                        placeholder="간단한 자기소개를 작성해주세요."
                    ></textarea>
                </div>

                <div class="flex justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                    <button
                        type="button"
                        onclick={cancelEdit}
                        class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        취소
                    </button>
                    <button
                        type="submit"
                        disabled={isLoading}
                        class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 disabled:opacity-50 transition-colors flex items-center gap-2"
                    >
                        {#if isLoading}
                            <Icon name="Loader2" size="sm" class="animate-spin" />
                        {/if}
                        저장
                    </button>
                </div>
            </form>
        {:else}
            <!-- 보기 모드 -->
            <div class="p-6">
                <dl class="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div>
                        <dt class="text-sm text-gray-500 dark:text-gray-400">이름</dt>
                        <dd class="mt-1 text-sm font-medium text-gray-900 dark:text-white">{user.name}</dd>
                    </div>
                    <div>
                        <dt class="text-sm text-gray-500 dark:text-gray-400">이메일</dt>
                        <dd class="mt-1 text-sm font-medium text-gray-900 dark:text-white">{user.email}</dd>
                    </div>
                    <div>
                        <dt class="text-sm text-gray-500 dark:text-gray-400">연락처</dt>
                        <dd class="mt-1 text-sm font-medium text-gray-900 dark:text-white">{user.phone || '-'}</dd>
                    </div>
                    <div>
                        <dt class="text-sm text-gray-500 dark:text-gray-400">회사/소속</dt>
                        <dd class="mt-1 text-sm font-medium text-gray-900 dark:text-white">{user.company || '-'}</dd>
                    </div>
                    <div>
                        <dt class="text-sm text-gray-500 dark:text-gray-400">부서</dt>
                        <dd class="mt-1 text-sm font-medium text-gray-900 dark:text-white">{user.department || '-'}</dd>
                    </div>
                    <div>
                        <dt class="text-sm text-gray-500 dark:text-gray-400">직책</dt>
                        <dd class="mt-1 text-sm font-medium text-gray-900 dark:text-white">{user.position || '-'}</dd>
                    </div>
                    <div class="md:col-span-2">
                        <dt class="text-sm text-gray-500 dark:text-gray-400">자기소개</dt>
                        <dd class="mt-1 text-sm text-gray-900 dark:text-white">{user.bio || '-'}</dd>
                    </div>
                </dl>
            </div>
        {/if}
    </div>
</div>