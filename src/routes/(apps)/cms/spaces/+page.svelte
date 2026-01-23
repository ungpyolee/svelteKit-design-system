<script>
    /**
     * CMS - Space 목록 페이지
     * Space: 콘텐츠 공간 (Blog, Tech Docs, Wiki 등)
     */
    import Icon from "$lib/icons/icon.svelte";
    import { Modal, Pagination } from "$lib/components/ui";

    let spaces = $state([
        {
            id: 1,
            name: 'Tech Blog',
            description: '기술 블로그 - 개발 관련 아티클',
            type: 'Open',
            creatorName: '김철수',
            createdAt: '2024-01-15T09:00:00.000Z',
            contentCount: 45,
            memberCount: 8
        },
        {
            id: 2,
            name: 'Product Docs',
            description: '제품 문서 - 사용자 가이드 및 API 문서',
            type: 'Moderated',
            creatorName: '이민수',
            createdAt: '2024-02-20T10:30:00.000Z',
            contentCount: 128,
            memberCount: 12
        },
        {
            id: 3,
            name: 'Internal Wiki',
            description: '사내 위키 - 내부 정책 및 프로세스',
            type: 'Closed',
            creatorName: '박지영',
            createdAt: '2024-03-10T14:20:00.000Z',
            contentCount: 67,
            memberCount: 5
        },
    ]);

    // SpaceType별 스타일 및 설명
    const typeConfig = {
        'Open': {
            class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400',
            desc: '모든 인증 사용자가 작성/발행 가능'
        },
        'Moderated': {
            class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400',
            desc: '모든 사용자 작성 가능, Editor만 발행'
        },
        'Closed': {
            class: 'bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300',
            desc: 'Space 멤버만 작성/발행 가능'
        },
    };

    let showDetailModal = $state(false);
    let selectedSpace = $state(null);

    let currentPage = $state(1);
    const totalItems = 12;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }

    function handleRowClick(space) {
        selectedSpace = space;
        showDetailModal = true;
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Space 관리</p>
            <a href="/apps/cms/spaces/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                <Icon name="Plus" size="sm" class="inline mr-1" />
                Space 추가
            </a>
        </header>

        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Space</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">타입</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">콘텐츠</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">멤버</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">생성일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each spaces as space}
                        <tr
                            class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors cursor-pointer"
                            onclick={() => handleRowClick(space)}
                        >
                            <td class="px-4 py-3">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                                        <Icon name="Layers" size="sm"/>
                                    </div>
                                    <div>
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{space.name}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400 truncate max-w-[200px]">{space.description}</p>
                                    </div>
                                </div>
                            </td>
                            <td class="px-4 py-3">
                                <span class="px-2 py-1 text-xs font-medium {typeConfig[space.type]?.class} rounded-full">
                                    {space.type}
                                </span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {space.contentCount}개
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {space.memberCount}명
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(space.createdAt)}
                            </td>
                            <td class="px-4 py-3">
                                <button
                                    class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                    onclick={(e) => { e.stopPropagation(); handleRowClick(space); }}
                                >
                                    <Icon name="ChevronRight" size="sm"/>
                                </button>
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

<!-- 상세 모달 -->
<Modal bind:isOpen={showDetailModal} title="Space 정보" size="md">
    {#if selectedSpace}
        <div class="space-y-4">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                    <Icon name="Layers" size="md"/>
                </div>
                <div>
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-200">{selectedSpace.name}</p>
                    <p class="text-sm text-gray-500">{selectedSpace.description}</p>
                </div>
            </div>

            <div class="grid grid-cols-2 gap-4">
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">타입</p>
                    <span class="px-2 py-1 text-xs font-medium {typeConfig[selectedSpace.type]?.class} rounded-full">
                        {selectedSpace.type}
                    </span>
                    <p class="text-xs text-gray-500 mt-1">{typeConfig[selectedSpace.type]?.desc}</p>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">생성자</p>
                    <p class="text-sm text-gray-800 dark:text-gray-200">{selectedSpace.creatorName}</p>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">콘텐츠 수</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedSpace.contentCount}개</p>
                </div>
                <div class="p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                    <p class="text-xs text-gray-500 dark:text-gray-400 mb-1">멤버 수</p>
                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedSpace.memberCount}명</p>
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
            href="/apps/cms/spaces/{selectedSpace?.id}/edit"
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            상세/수정
        </a>
    {/snippet}
</Modal>
