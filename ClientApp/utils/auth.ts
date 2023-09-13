import {NitroFetchOptions} from "nitropack";

const getAuthorizationHeader = (token: string | null, defaultHeaders?: { [key: string]: any }) => {
    return {
        ...defaultHeaders,
        Authorization: `Bearer ${token}`
    };
};

const $authFetch = async <T>(url: string, opts: NitroFetchOptions<any> = {}) => {
    return await $fetch<T>(url, {
        baseURL: '/_',
        headers: getAuthorizationHeader(useAuth().accessToken.value),
        ...opts
    });
};

export {$authFetch}