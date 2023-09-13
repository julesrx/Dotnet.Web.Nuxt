type JwtPayload = { exp: number; sub: string; email: string };
type User = { email: string };

const createStore = () => {
    const jwt = ref<string | null>(localStorage.getItem('jwt-token'));

    watch(jwt, (jwt) => {
        if (!jwt) return;
        localStorage.setItem('jwt-token', jwt);
    });

    const payload = computed<JwtPayload | null>(() => (!jwt.value ? null : parseJwt(jwt.value)));
    const user = computed<User | null>(() =>
        !payload.value
            ? null
            : {
                email: payload.value.email,
            },
    );

    const isAuthenticated = computed(() => !!user.value);
    const isTokenExpired = computed(() => !payload.value || Date.now() > payload.value.exp * 1000);

    const login = async () => {
        jwt.value = await $fetch<string>('/_/auth/token', {method: 'POST'});
    };

    return {
        user,
        isAuthenticated,
        isTokenExpired,
        login,
    };
};

function parseJwt(token: string): JwtPayload {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
        window
            .atob(base64)
            .split('')
            .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
            .join(''),
    );

    return JSON.parse(jsonPayload);
}

const store = createStore()

export default () => store;
