import { render } from 'https://cdn.skypack.dev/solid-js/web';
import { createSignal, createResource } from 'https://cdn.skypack.dev/solid-js';
import html from "https://cdn.skypack.dev/solid-js/html";

const App = () => {
    const [query, setQuery] = createSignal("");

    const [showDropdown, setShowDropdown] = createSignal(false);


    const fetchData = async (searchTerm) => {
        if (!searchTerm || !searchTerm.trim().length) return { "matches": [] };
        return (await fetch(`/api/products/?query=${searchTerm}`)).json();
    }

    const [searchResults] = createResource(query, fetchData);


    return html`
            <input
                type="text"
                class="form-control"
                min="1"
                placeholder="Enter property name"
                value=${query}
                onInput=${(e) => {
                    setQuery(e.currentTarget.value);
                    setShowDropdown(false);
                }}
            />
        ${() => {
        if (searchResults.loading) 
            return html`<p>Loading..</p>`
        if (searchResults() && searchResults().matches && searchResults().matches.length > 0) { setShowDropdown(true) }
            return () => {
                if (showDropdown()) {
                    const data = searchResults().matches;
                    return data.map((item, i) => html`
                            <div class="mt-3 text-bg-light border border-dark-subtle rounded-1 bg-dark-subtle">
                              <div class="row">
                               <div class="col">Product Name</div>
                               <div class="col">Product ID</div>
                               <div class="col">Full Name</div>
                               <div class="col">Project Group ID</div>
                               <div class="col">Metro Area Title</div>
                              </div>
                               <div class="row text-secondary">
                               <div class="col">${item.productName}</div>
                               <div class="col">${item.productID}</div>
                               <div class="col">${item.fullName}</div>
                               <div class="col">${item.projectGroupID}</div>
                               <div class="col">${item.metroAreaTitle}</div>
                              </div>
                          </div>
                        `)
                }
                return html`<p>No Results Found</p>`;
            }
    }}
    `;
};

render(App, document.getElementById('solid-search-component'));