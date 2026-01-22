<script>
    /**
     * 사용자 관리 - 생성 페이지
     *
     * [페이지 설명]
     * - 새 사용자를 추가하기 위한 폼 페이지
     * - 기본 정보 입력 + 역할/팀 배정 영역으로 구성
     *
     * [주의사항]
     * - 외부 IDP 사용 시 이 페이지 대신 초대 링크 발송 방식으로 변경될 수 있음
     * - 현재는 UI 스켈레톤만 구현된 상태
     */

    import Icon from "$lib/icons/icon.svelte";

    /**
     * 폼 데이터
     * 실제 구현 시 API 요청 body로 사용됨
     */
    let formData = $state({
        displayName: '',    // 이름
        email: '',          // 이메일
        phone: '',          // 연락처
        affiliation: '',    // 소속
    });

    /**
     * 폼 제출 핸들러
     * TODO: 실제 API 연동 시 구현 필요
     */
    function handleSubmit(e) {
        e.preventDefault();
        console.log('Create user:', formData);
        // TODO: API 호출 후 목록 페이지로 이동
    }
</script>

<!-- ==================== 메인 컨텐츠 ==================== -->
<!-- max-w-2xl: 폼 페이지는 좁은 너비로 제한 -->
<div class="max-w-2xl mx-auto">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">

        <!-- ===== 페이지 헤더 ===== -->
        <!-- 뒤로가기 버튼 + 제목 -->
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center gap-3">
                <!-- 뒤로가기: 목록 페이지로 이동 -->
                <a
                    href="/admin/users"
                    class="p-2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="ArrowLeft" size="sm" />
                </a>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">사용자 추가</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">새 사용자 정보를 입력하세요</p>
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
                            placeholder="홍길동"
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
                            placeholder="user@company.com"
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
                            placeholder="010-1234-5678"
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
                            placeholder="회사/조직명"
                            class="w-full px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                        />
                    </div>
                </div>
            </div>

            <!-- ===== 역할 & 팀 배정 섹션 ===== -->
            <!-- TODO: 추후 구현 필요한 영역 -->
            <div class="space-y-4">
                <h3 class="text-sm font-medium text-gray-800 dark:text-gray-200">역할 & 팀 배정</h3>
                <p class="text-xs text-gray-500 dark:text-gray-400">TODO: 역할 선택 및 팀 배정 기능 추가 예정</p>

                <!-- 플레이스홀더 영역 - 실제 UI로 교체 필요 -->
                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg border-2 border-dashed border-gray-200 dark:border-gray-700">
                    <p class="text-sm text-gray-500 dark:text-gray-400 text-center">역할 선택 및 팀 배정 UI 영역</p>
                </div>
            </div>

            <!-- ===== 하단 버튼 영역 ===== -->
            <div class="flex items-center justify-end gap-3 pt-4 border-t border-gray-100 dark:border-gray-800">
                <!-- 취소 버튼: 목록으로 돌아가기 -->
                <a
                    href="/admin/users"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    취소
                </a>
                <!-- 제출 버튼 -->
                <button
                    type="submit"
                    class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                    사용자 추가
                </button>
            </div>
        </form>
    </div>
</div>
