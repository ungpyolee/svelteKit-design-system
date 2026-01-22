<script>
    /**
     * CMS - 콘텐츠 목록 페이지
     * Space 필터, 상태 필터, 검색 기능
     */
    import Icon from "$lib/icons/icon.svelte";
    import { Pagination } from "$lib/components/ui";

    let contents = $state([
        {
            id: 1,
            title: 'SvelteKit 5 새로운 기능 소개',
            summary: 'SvelteKit 5에서 추가된 주요 기능들을 살펴봅니다.',
            slug: 'sveltekit-5-new-features',
            status: 'Published',
            spaceName: 'Tech Blog',
            spaceId: 1,
            authorName: '김철수',
            coverImageUrl: null,
            viewCount: 1234,
            publishedAt: '2024-01-15T09:00:00.000Z',
            createdAt: '2024-01-10T09:00:00.000Z',
        },
        {
            id: 2,
            title: 'ASP.NET Core Identity 가이드',
            summary: 'ASP.NET Core Identity를 활용한 인증/인가 구현 방법',
            slug: 'aspnet-core-identity-guide',
            status: 'Draft',
            spaceName: 'Tech Blog',
            spaceId: 1,
            authorName: '이민수',
            coverImageUrl: null,
            viewCount: 0,
            publishedAt: null,
            createdAt: '2024-02-20T10:30:00.000Z',
        },
        {
            id: 3,
            title: 'API 사용자 가이드',
            summary: '제품 API 사용 방법에 대한 상세 가이드',
            slug: 'api-user-guide',
            status: 'Published',
            spaceName: 'Product Docs',
            spaceId: 2,
            authorName: '박지영',
            coverImageUrl: null,
            viewCount: 567,
            publishedAt: '2024-03-10T14:20:00.000Z',
            createdAt: '2024-03-05T14:20:00.000Z',
        },
        {
            id: 4,
            title: '온보딩 프로세스',
            summary: '신규 입사자를 위한 온보딩 절차 안내',
            slug: 'onboarding-process',
            status: 'Archived',
            spaceName: 'Internal Wiki',
            spaceId: 3,
            authorName: '김철수',
            coverImageUrl: null,
            viewCount: 89,
            publishedAt: '2023-12-01T09:00:00.000Z',
            createdAt: '2023-11-25T09:00:00.000Z',
        },
    ]);

    // Space 목록 (필터용)
    let spaces = $state([
        { id: 0, name: '전체' },
        { id: 1, name: 'Tech Blog' },
        { id: 2, name: 'Product Docs' },
        { id: 3, name: 'Internal Wiki' },
    ]);

    const statusConfig = {
        'Temporary': { class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400', icon: 'FileEdit' },
        'Draft': { class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400', icon: 'FilePen' },
        'Published': { class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400', icon: 'FileCheck' },
        'Archived': { class: 'bg-gray-100 text-gray-500 dark:bg-gray-800 dark:text-gray-400', icon: 'FileArchive' },
    };

    // 필터
    let selectedSpaceId = $state(0);
    let selectedStatus = $state('');
    let searchQuery = $state('');

    // 페이지네이션
    let currentPage = $state(1);
    const totalItems = 156;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function formatDate(dateString) {
        if (!dateString) return '-';
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }

    function handleSearch(e) {
        e.preventDefault();
        console.log('Search:', { spaceId: selectedSpaceId, status: selectedStatus, query: searchQuery });
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">콘텐츠 관리</p>
            <a href="/apps/cms/contents/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                <Icon name="Plus" size="sm" class="inline mr-1" />
                새 콘텐츠
            </a>
        </header>

        <!-- 필터 영역 -->
        <div class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 bg-gray-50 dark:bg-gray-800/50">
            <form onsubmit={handleSearch} class="flex items-center gap-4">
                <!-- Space 필터 -->
                <select
                    bind:value={selectedSpaceId}
                    class="px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                >
                    {#each spaces as space}
                        <option value={space.id}>{space.name}</option>
                    {/each}
                </select>

                <!-- Status 필터 -->
                <select
                    bind:value={selectedStatus}
                    class="px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                >
                    <option value="">전체 상태</option>
                    <option value="Draft">Draft</option>
                    <option value="Published">Published</option>
                    <option value="Archived">Archived</option>
                </select>

                <!-- 검색 -->
                <div class="flex-1 flex items-center gap-2">
                    <input
                        type="text"
                        bind:value={searchQuery}
                        placeholder="제목으로 검색"
                        class="flex-1 px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                    />
                    <button
                        type="submit"
                        class="p-2 text-gray-500 hover:text-primary hover:bg-white dark:hover:bg-gray-800 rounded-lg transition-colors"
                    >
                        <Icon name="Search" size="sm" />
                    </button>
                </div>
            </form>
        </div>

        <!-- 테이블 -->
        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">콘텐츠</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Space</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">조회수</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">발행일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each contents as content}
                        <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                            <td class="px-4 py-3">
                                <div class="flex items-start gap-3">
                                    <div class="w-8 h-8 rounded-lg bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-600 dark:text-gray-400">
                                        <Icon name="FileText" size="sm"/>
                                    </div>
                                    <div class="flex-1 min-w-0">
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200 truncate">{content.title}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400 truncate">{content.summary}</p>
                                    </div>
                                </div>
                            </td>
                            <td class="px-4 py-3">
                                <span class="text-sm text-gray-600 dark:text-gray-400">{content.spaceName}</span>
                            </td>
                            <td class="px-4 py-3">
                                <span class="px-2 py-1 text-xs font-medium {statusConfig[content.status]?.class} rounded-full">
                                    {content.status}
                                </span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {content.viewCount.toLocaleString()}
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(content.publishedAt)}
                            </td>
                            <td class="px-4 py-3">
                                <a
                                    href="/apps/cms/contents/{content.id}/edit"
                                    class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors inline-block"
                                >
                                    <Icon name="Pencil" size="sm"/>
                                </a>
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>

        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination bind:currentPage={currentPage} {totalPages} {totalItems} {itemsPerPage} />
        </footer>
    </div>
</div>
