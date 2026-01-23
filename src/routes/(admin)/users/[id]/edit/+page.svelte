<script>
    /**
     * 사용자 관리 - 수정 페이지
     *
     * [페이지 설명]
     * - 기존 사용자의 정보를 수정하는 폼 페이지
     * - 기본 정보 수정 + 상태 변경 + 역할 변경 + 삭제 기능
     *
     * [URL 구조]
     * - /admin/users/[id]/edit 형태
     * - 예: /admin/users/3fa85f64-5717-4562-b3fc-2c963f66afa6/edit
     */

    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    /**
     * URL에서 id 파라미터 가져오기
     */
    let userId = $derived($page.params.id);

    /**
     * 폼 데이터
     * 실제 구현 시 API에서 기존 데이터를 조회해서 채움
     */
    let formData = $state({
        displayName: '김철수',
        email: 'cs.kim@company.com',
        phone: '010-1234-5678',
        affiliation: 'CLEW',
        status: 'Active',
    });

    /**
     * 상태 옵션
     * 드롭다운 선택지로 사용
     */
    const statusOptions = [
        { value: 'Active', label: 'Active' },
        { value: 'Inactive', label: 'Inactive' },
        { value: 'Pending', label: 'Pending' },
    ];

    /**
     * 폼 제출 핸들러
     * TODO: 실제 API 연동 시 구현 필요
     */
    function handleSubmit(e) {
        e.preventDefault();
        console.log('Update user:', userId, formData);
        // TODO: API 호출 후 상세 페이지로 이동
    }

    /**
     * 사용자 삭제 핸들러
     * TODO: 실제 API 연동 시 구현 필요
     */
    function handleDelete() {
        if (confirm('정말 이 사용자를 삭제하시겠습니까?')) {
            console.log('Delete user:', userId);
            // TODO: API 호출 후 목록 페이지로 이동
        }
    }
</script>

<!-- ==================== 메인 컨텐츠 ==================== -->
<!-- max-w-2xl: 폼 페이지는 좁은 너비로 제한 -->
<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">

        <!-- ===== 페이지 헤더 ===== -->
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <!-- 뒤로가기: 상세 페이지로 이동 -->
                <a
                    href="/admin/users/{userId}"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">사용자 정보 수정</p>
                    <!-- 현재 수정 중인 사용자 이름 표시 -->
                    <p class="text-sm text-gray-500 dark:text-gray-400">{formData.displayName}</p>
                </div>
            </div>
        </header>

        <!-- ===== 폼 영역 ===== -->
        <form onsubmit={handleSubmit} class="p-6 space-y-6">

            <!-- 기본 정보 섹션 -->
            <div class="space-y-4">
                <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">기본 정보</h3>

                <!-- 이름 + 이메일 (2열) -->
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            이름 <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="text"
                            bind:value={formData.displayName}
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            이메일 <span class="text-red-500">*</span>
                        </label>
                        <input
                            type="email"
                            bind:value={formData.email}
                            required
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                </div>

                <!-- 연락처 + 소속 (2열) -->
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            연락처
                        </label>
                        <input
                            type="tel"
                            bind:value={formData.phone}
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                            소속
                        </label>
                        <input
                            type="text"
                            bind:value={formData.affiliation}
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                </div>
            </div>

            <!-- ===== 상태 변경 섹션 ===== -->
            <div class="space-y-4">
                <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">상태</h3>

                <div>
                    <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                        사용자 상태
                    </label>
                    <!-- 상태 드롭다운 -->
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

            <!-- ===== 역할 변경 섹션 ===== -->
            <!-- TODO: 추후 구현 필요 -->
            <div class="space-y-4">
                <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">역할 변경</h3>
                <!-- 플레이스홀더 - 실제 역할 선택 UI로 교체 필요 -->
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                    <p class="text-sm text-gray-500 dark:text-gray-400 text-center">역할 변경 UI 영역 (TODO)</p>
                </div>
            </div>

            <!-- ===== 하단 버튼 영역 ===== -->
            <div class="flex items-center justify-between pt-4 border-t border-gray-100 dark:border-gray-800">
                <!-- 좌측: 삭제 버튼 (위험 액션) -->
                <button
                    type="button"
                    onclick={handleDelete}
                    class="px-4 py-2 text-sm font-medium text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                >
                    사용자 삭제
                </button>

                <!-- 우측: 취소 + 저장 버튼 -->
                <div class="flex items-center gap-3">
                    <a
                        href="/admin/users/{userId}"
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
</div>
