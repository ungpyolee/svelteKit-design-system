<!-- /routes/(apps)/rms-admin/faq/+page.svelte -->
<script>
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";
    import { Breadcrumb, PageHeader, Pagination } from "$lib/components/ui";
    import FaqTable from "../FaqTable.svelte";

    // 더미 데이터
    let faqs = $state([
        {
            faqId: 'FAQ-001',
            createdDate: '2024-01-20',
            question: '기술자료는 어떻게 구매하나요?',
            answer: '원하는 기술자료를 선택한 후 결제하시면 즉시 다운로드 가능합니다.',
            isPublished: true,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            updatedDate: '2024-01-20'
        },
        {
            faqId: 'FAQ-002',
            createdDate: '2024-01-19',
            question: '기여자로 등록하려면 어떻게 해야 하나요?',
            answer: '회원가입 후 마이페이지에서 기여자 신청을 하시면 됩니다. 심사 후 승인되면 기술자료를 등록하실 수 있습니다.',
            isPublished: true,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            updatedDate: '2024-01-19'
        },
        {
            faqId: 'FAQ-003',
            createdDate: '2024-01-18',
            question: '환불 정책은 어떻게 되나요?',
            answer: '구매 후 7일 이내, 다운로드 이력이 없는 경우 전액 환불 가능합니다.',
            isPublished: true,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            updatedDate: '2024-01-18'
        },
        {
            faqId: 'FAQ-004',
            createdDate: '2024-01-17',
            question: '기술자료의 저작권은 누구에게 있나요?',
            answer: '기술자료의 저작권은 원 저작자(기여자)에게 있으며, 구매자는 개인적/업무적 용도로만 사용 가능합니다.',
            isPublished: true,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            updatedDate: '2024-01-17'
        },
        {
            faqId: 'FAQ-005',
            createdDate: '2024-01-16',
            question: '무료 기술자료는 어떻게 이용하나요?',
            answer: '무료 기술자료는 기여자에게 요청을 보내고 승인을 받으면 열람 가능합니다.',
            isPublished: false,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            updatedDate: '2024-01-16'
        },
        {
            faqId: 'FAQ-006',
            createdDate: '2024-01-15',
            question: '대량 구매 할인이 가능한가요?',
            answer: '10건 이상 대량 구매 시 별도 협의 가능합니다. 영업팀(sales@rms.com)으로 문의해주세요.',
            isPublished: false,
            authorName: '관리자',
            authorEmail: 'admin@rms.com',
            updatedDate: '2024-01-15'
        },
    ]);

    // 필터 상태
    let publishFilter = $state('all');

    // 필터링된 데이터
    let filteredFaqs = $derived(
        publishFilter === 'all'
            ? faqs
            : publishFilter === 'published'
                ? faqs.filter(f => f.isPublished)
                : faqs.filter(f => !f.isPublished)
    );

    // 상태별 건수
    let publishedCount = $derived(faqs.filter(f => f.isPublished).length);
    let unpublishedCount = $derived(faqs.filter(f => !f.isPublished).length);

    // 페이지네이션
    let currentPage = $state(1);
    const itemsPerPage = 10;
    let totalPages = $derived(Math.ceil(filteredFaqs.length / itemsPerPage));

    // 이벤트 핸들러
    function handleEdit(faq) {
        goto(`/rms-admin/faq/${faq.faqId}/edit`);
    }

    function handleDelete(faq) {
        // API 호출
        faqs = faqs.filter(f => f.faqId !== faq.faqId);
        console.log('Deleted:', faq.faqId);
    }

    function handleTogglePublish(faq) {
        // API 호출
        const index = faqs.findIndex(f => f.faqId === faq.faqId);
        if (index !== -1) {
            faqs[index].isPublished = !faqs[index].isPublished;
        }
        console.log('Toggled publish:', faq.faqId, faqs[index].isPublished);
    }
</script>

<div class="space-y-6">
    <Breadcrumb 
        items={[
            { label: 'RMS 관리', href: '/rms-admin/dashboard' },
            { label: 'FAQ 관리' }
        ]} 
    />

    <PageHeader 
        title="FAQ 관리" 
        description="자주 묻는 질문을 관리합니다."
        icon="CircleHelp"
    >
        <a
            href="/rms-admin/faq/new"
            class="px-4 py-2 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary-600 transition-colors flex items-center gap-2"
        >
            <Icon name="Plus" size="sm" />
            신규 FAQ 등록
        </a>
    </PageHeader>

    <!-- 통계 카드 -->
    <div class="grid grid-cols-3 gap-4">
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-purple-100 dark:bg-purple-900 flex items-center justify-center">
                    <Icon name="HelpCircle" size="md" class="text-purple-600 dark:text-purple-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{faqs.length}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">전체 FAQ</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-green-100 dark:bg-green-900 flex items-center justify-center">
                    <Icon name="Eye" size="md" class="text-green-600 dark:text-green-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{publishedCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">게시중</p>
                </div>
            </div>
        </div>
        <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-5">
            <div class="flex items-center gap-4">
                <div class="w-11 h-11 rounded-xl bg-gray-100 dark:bg-gray-800 flex items-center justify-center">
                    <Icon name="EyeOff" size="md" class="text-gray-600 dark:text-gray-400" />
                </div>
                <div>
                    <p class="text-2xl font-bold text-gray-900 dark:text-white">{unpublishedCount}</p>
                    <p class="text-sm text-gray-500 dark:text-gray-400">비공개</p>
                </div>
            </div>
        </div>
    </div>

    <!-- 필터 탭 + 테이블 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <div class="flex items-center justify-between">
                <div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg">
                    <button
                        onclick={() => publishFilter = 'all'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {publishFilter === 'all' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        전체
                        <span class="ml-1 text-xs text-gray-400">({faqs.length})</span>
                    </button>
                    <button
                        onclick={() => publishFilter = 'published'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {publishFilter === 'published' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        게시중
                        <span class="ml-1 text-xs text-gray-400">({publishedCount})</span>
                    </button>
                    <button
                        onclick={() => publishFilter = 'unpublished'}
                        class="px-4 py-2 text-sm font-medium rounded-md transition-colors {publishFilter === 'unpublished' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                    >
                        비공개
                        <span class="ml-1 text-xs text-gray-400">({unpublishedCount})</span>
                    </button>
                </div>
            </div>
        </header>
        
        <section class="p-6">
            <FaqTable 
                data={filteredFaqs}
                onEdit={handleEdit}
                onDelete={handleDelete}
                onTogglePublish={handleTogglePublish}
                emptyMessage={publishFilter === 'published' ? '게시중인 FAQ가 없습니다.' : publishFilter === 'unpublished' ? '비공개 FAQ가 없습니다.' : '등록된 FAQ가 없습니다.'}
            />
        </section>
        
        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination 
                bind:currentPage={currentPage}
                totalPages={totalPages}
                totalItems={filteredFaqs.length}
                itemsPerPage={itemsPerPage}
            />
        </footer>
    </div>
</div>