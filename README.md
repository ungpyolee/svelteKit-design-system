# Clew Design System

SvelteKit 기반의 우리회사 통합 UI/Design System 프로젝트입니다. 각종 개발 프로젝트의 백오피스 페이지에 활용가능합니다.


## 기술 스택

- **Framework**: SvelteKit 2.49.1
- **UI Library**: Svelte 5.45.6
- **Styling**: Tailwind CSS 3.4.19
- **Icons**: Lucide Svelte
- **Utilities**: clsx, tailwind-merge, zod
- **Font**: Pretendard (한글 지원)

## 주요 기능

- 라이트/다크 모드 테마 지원
- 반응형 사이드바 레이아웃
- 기술자료(Digital Assets) 관리 시스템
- 팀 관리 및 역할 기반 권한 시스템
- 재사용 가능한 UI 컴포넌트 라이브러리

## 프로젝트 구조

```
src/
├── lib/
│   ├── actions/          # Svelte 액션 (clickOutside 등)
│   ├── assets/           # 로고 및 이미지 에셋
│   ├── components/
│   │   ├── layouts/      # Navbar, Sidebar 레이아웃 컴포넌트
│   │   └── ui/           # Button, Input, Modal 등 UI 컴포넌트
│   ├── config/           # 메뉴 및 데이터 설정
│   ├── icons/            # 아이콘 래퍼 컴포넌트
│   ├── stores/           # Theme, Sidebar 상태 관리
│   └── utils/            # 유틸리티 함수 (cn 등)
├── routes/
│   ├── auth/             # 인증 페이지
│   ├── components/       # 컴포넌트 데모 페이지
│   ├── dashboard/        # 대시보드
│   ├── forms/            # 폼 관련 페이지
│   ├── rms/              # RMS 핵심 페이지
│   ├── setting/          # 설정 페이지
│   └── tables/           # 테이블 데모 페이지
└── static/
    └── fonts/            # 폰트 파일
```

## UI 컴포넌트

| 컴포넌트 | 설명 |
|---------|------|
| Button | 다양한 스타일 지원 버튼 |
| Input | 입력 필드 |
| Modal | 모달 다이얼로그 (sm/md/lg/xl/2xl 크기) |
| Pagination | 페이지네이션 |
| ThemeToggle | 라이트/다크 모드 토글 |
| Navbar | 상단 네비게이션 바 |
| Sidebar | 좌측 사이드바 메뉴 |

## 페이지 구조

### RMS (`/rms`)
- `/rms/user-dashboard` - 사용자 대시보드
- `/rms/digital-assets` - 기술자료 목록
- `/rms/digital-assets/new` - 새 기술자료 작성
- `/rms/digital-assets/[id]` - 기술자료 상세
- `/rms/digital-assets/[id]/edit` - 기술자료 수정
- `/rms/digital-asset-requests` - 자산 요청 목록
- `/rms/digital-asset-payments` - 자산 결제 목록

### Setting (`/setting`)
- `/setting/manage-teams` - 팀 관리
- `/setting/switch-team` - 팀 전환

### Components Demo (`/components`)
- `/components/typography` - 타이포그래피
- `/components/card` - 카드
- `/components/modal` - 모달
- `/components/colors` - 색상 팔레트

## 설치 및 실행

```bash
# 의존성 설치
npm install

# 개발 서버 실행
npm run dev

# 프로덕션 빌드
npm run build

# 빌드 미리보기
npm run preview
```

## 메뉴 설정

사이드바 메뉴는 `src/lib/config/menu.js`에서 설정합니다. 메뉴 타입은 3가지입니다:

| 타입 | 설명 |
|------|------|
| `title` | 섹션 구분 타이틀 (클릭 불가) |
| `menu` | 단일 메뉴 항목 |
| `dropdown-menu` | 서브메뉴가 있는 드롭다운 메뉴 |

### 메뉴 설정 예시

```javascript
// src/lib/config/menu.js
export const menuConfig = [
    // 섹션 타이틀
    {
        type: "title",
        innerText: "UI Elements",
    },
    // 드롭다운 메뉴 (서브메뉴 포함)
    {
        type: "dropdown-menu",
        name: "components",
        innerText: "Components",
        lucideIcon: "FileText",      // Lucide 아이콘 이름
        submenuList: [
            {
                name: "typography",
                innerText: "Typography",
                url: "/components/typography",
            },
            {
                name: "card",
                innerText: "Card",
                url: "/components/card",
            },
        ]
    },
    // 단일 메뉴
    {
        type: "menu",
        name: "test",
        innerText: "Test",
        url: "/test",
        lucideIcon: "FileText",
    },
]
```

### 메뉴 속성

| 속성 | 필수 | 설명 |
|------|------|------|
| `type` | O | 메뉴 타입 (`title`, `menu`, `dropdown-menu`) |
| `name` | O | 메뉴 식별자 (title 제외) |
| `innerText` | O | 화면에 표시될 텍스트 |
| `url` | O | 이동할 경로 (`menu` 타입만) |
| `lucideIcon` | - | Lucide 아이콘 이름 |
| `submenuList` | O | 서브메뉴 배열 (`dropdown-menu` 타입만) |

## 테마 설정

CSS 변수 기반의 테마 시스템을 사용합니다. `src/app.css`에서 라이트/다크 테마 색상을 정의합니다.

```css
:root {
  --color-primary: #3b82f6;
  --color-background: #ffffff;
  --color-foreground: #0f172a;
}

.dark {
  --color-primary: #60a5fa;
  --color-background: #161630;
  --color-foreground: #f8fafc;
}
```

## 라이선스

Private
