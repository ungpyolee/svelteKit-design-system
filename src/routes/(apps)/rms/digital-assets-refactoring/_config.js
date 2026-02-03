// /routes/(apps)/rms/digital-assets/_config.js

export const assetTypeOptions = ['설계도면', '사양서', '시험성적서', '기술문서', '해석보고서'];
export const motorTypeOptions = ['IPMSM', 'SPMSM', 'IM', 'BLDC', 'SRM'];

export const sortOptions = [
    { value: 'salesDesc', label: '판매 많은 순' },
    { value: 'salesAsc', label: '판매 적은 순' },
    { value: 'priceDesc', label: '가격 높은 순' },
    { value: 'priceAsc', label: '가격 낮은 순' },
    { value: 'dateDesc', label: '최신 등록순' },
    { value: 'dateAsc', label: '오래된 순' },
];

export const statusOptions = [
    { value: 'all', label: '전체' },
    { value: 'draft', label: '임시저장' },
    { value: 'pending', label: '승인대기', highlight: true },
    { value: 'published', label: '등록' },
    { value: 'unpublished', label: '미등록' },
    { value: 'rejected', label: '반려' },
];

export const statusConfig = {
    draft: { label: '임시저장', color: 'slate', icon: 'FileEdit' },
    pending: { label: '승인대기', color: 'amber', icon: 'Clock' },
    published: { label: '등록', color: 'green', icon: 'Eye' },
    unpublished: { label: '미등록', color: 'gray', icon: 'EyeOff' },
    rejected: { label: '반려', color: 'red', icon: 'XCircle' },
};