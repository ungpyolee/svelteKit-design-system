<script>
    /**
     * 사용자 관리 - 상세 페이지
     *
     * [페이지 설명]
     * - 개별 사용자의 상세 정보를 보여주는 페이지
     * - URL 파라미터로 userId를 받아서 해당 사용자 정보 표시
     * - 기본 정보 + 시스템 정보 + 활동 이력 + 소속 팀 목록으로 구성
     *
     * [URL 구조]
     * - /admin/users/[id] 형태
     * - 예: /admin/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
     */

    import { page } from '$app/stores';
    import Icon from "$lib/icons/icon.svelte";

    /**
     * URL에서 id 파라미터 가져오기
     * $derived: Svelte 5의 반응형 파생 값
     */
    let userId = $derived($page.params.id);

    /**
     * 샘플 사용자 데이터
     * 실제 구현 시 userId를 이용해 API에서 조회
     */
    let user = $state({
        userId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        externalId: 'google-oauth2|123456789',
        displayName: '김철수',
        email: 'cs.kim@company.com',
        phone: '010-1234-5678',
        affiliation: 'CLEW',
        defaultTenantId: 'tenant_001',
        status: 'Active',
        createdAt: '2024-01-15T09:00:00.000Z'
    });

    /**
     * 상태별 스타일 설정
     */
    const statusConfig = {
        'Active': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400', dot: 'bg-success-500' },
        'Inactive': { class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400', dot: 'bg-gray-400' },
        'Pending': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400', dot: 'bg-warning-500' },
    };

    /**
     * 날짜 포맷 함수 (시간 포함)
     */
    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    }
</script>

<!-- ==================== 메인 컨텐츠 ==================== -->
<!-- max-w-4xl: 상세 페이지는 넓은 너비 사용 -->
<div class="max-w-4xl mx-auto space-y-6">

    <!-- ===== 사용자 정보 카드 ===== -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">

        <!-- 헤더: 뒤로가기 + 사용자 기본 정보 + 수정 버튼 -->
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center justify-between">
                <div class="flex items-center gap-3">
                    <!-- 뒤로가기 버튼 -->
                    <a
                        href="/admin/users"
                        class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        <Icon name="ArrowLeft" size="sm" />
                    </a>

                    <!-- 사용자 아바타 + 이름 + 이메일 -->
                    <div class="flex items-center gap-4">
                        <div class="w-12 h-12 rounded-full bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold text-lg">
                            {user.displayName.charAt(0)}
                        </div>
                        <div>
                            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">{user.displayName}</p>
                            <p class="text-sm text-gray-500 dark:text-gray-400">{user.email}</p>
                        </div>
                    </div>
                </div>

                <!-- 우측: 상태 배지 + 수정 버튼 -->
                <div class="flex items-center gap-2">
                    <span class="inline-flex items-center gap-1.5 px-2 py-1 text-xs font-medium {statusConfig[user.status]?.class} rounded-full">
                        <span class="w-1.5 h-1.5 {statusConfig[user.status]?.dot} rounded-full"></span>
                        {user.status}
                    </span>
                    <a
                        href="/admin/users/{userId}/edit"
                        class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-1"
                    >
                        <Icon name="Pencil" size="sm" />
                        수정
                    </a>
                </div>
            </div>
        </header>

        <!-- 정보 그리드 (2열) -->
        <section class="p-6">
            <div class="grid grid-cols-2 gap-6">

                <!-- 좌측: 기본 정보 -->
                <div class="space-y-4">
                    <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200">기본 정보</h3>
                    <div class="space-y-3">
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
                    </div>
                </div>

                <!-- 우측: 시스템 정보 -->
                <div class="space-y-4">
                    <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200">시스템 정보</h3>
                    <div class="space-y-3">
                        <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                            <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">User ID</p>
                            <code class="text-xs font-mono text-gray-600 dark:text-gray-300">{user.userId}</code>
                        </div>
                        <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                            <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">External ID (IDP)</p>
                            <code class="text-xs font-mono text-gray-600 dark:text-gray-300">{user.externalId}</code>
                        </div>
                        <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                            <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">Default Tenant ID</p>
                            <code class="text-xs font-mono text-gray-600 dark:text-gray-300">{user.defaultTenantId}</code>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- ===== 활동 이력 카드 ===== -->
    <!-- TODO: 추후 구현 필요 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <p class="text-sm font-semibold text-gray-800 dark:text-gray-100">활동 이력</p>
        </header>
        <section class="p-6">
            <!-- 플레이스홀더 - 실제 활동 이력 UI로 교체 필요 -->
            <div class="p-8 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                <p class="text-sm text-gray-500 dark:text-gray-400 text-center">활동 이력 UI 영역 (TODO)</p>
            </div>
        </section>
    </div>

    <!-- ===== 소속 팀 목록 카드 ===== -->
    <!-- TODO: 추후 구현 필요 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <p class="text-sm font-semibold text-gray-800 dark:text-gray-100">소속 팀 목록</p>
        </header>
        <section class="p-6">
            <!-- 플레이스홀더 - 실제 팀 목록 테이블로 교체 필요 -->
            <div class="p-8 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                <p class="text-sm text-gray-500 dark:text-gray-400 text-center">소속 팀 목록 UI 영역 (TODO)</p>
            </div>
        </section>
    </div>
</div>
