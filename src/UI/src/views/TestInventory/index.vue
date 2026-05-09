<template>
<body>
  <div class="inv" :style="'zoom:' + zoomDesign + ''">
  <main class="main">

    <div class="inventory">
      <div class="container">
        <h1 class="inventory__title">
      Inventory
        </h1>

        <div class="inventory__content">

          <div class="inventory__content-general general">
            <div class="general__block">

              <div class="general__gun gun">
                <div class="gun__info">
                  <div class="gun__img">
                    <img src="/img/testinv/svg/gun-ico.svg" alt="">
                  </div>
                  <div class="gun__text">
                    <h2 class="gun__title">
                  Weapon
                    </h2>
                    <p class="gun__descr">
                For the quick use of weapons
                    </p>
                  </div>
                </div>

                <div class="gun__slots">
                  <div class="gun__slot">
                    <div class="gun__step">
                      <h3>1</h3>
                    </div>

                    <div class="gun__item slot"></div>
                  </div>

                  <div class="gun__slot">
                    <div class="gun__step">
                      <h3>2</h3>
                    </div>

                    <div class="gun__item slot"></div>
                  </div>

                  <div class="gun__slot">
                    <div class="gun__step">
                      <h3>3</h3>
                    </div>

                    <div class="gun__item slot"></div>
                  </div>

                  <div class="gun__slot">
                    <div class="gun__step">
                      <h3>4</h3>
                    </div>

                    <div class="gun__item slot"></div>
                  </div>
                </div>
              </div>

              <div class="general__clothes clothes">
                <div class="clothes__info">
                  <div class="clothes__img">
                    <img src="/img/testinv/svg/clothes-ico.svg" alt="">
                  </div>
                  <div class="clothes__text">
                    <h2 class="clothes__title">
               Your things
                    </h2>
                    <p class="clothes__descr">
              Things on your character that can be removed and put on
                    </p>
                  </div>
                </div>

                <div class="clothes__slots">
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/cap-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/mardi-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/sunglasses-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/gloves-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/vest-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/ring-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/earrings-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/shirt-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/backpack-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/suit-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/pants-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/sweatshirt-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/watch-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/boots-ico.svg" alt="">
                    </div>
                  </div>
                  <div class="clothes__slot">
                    <div class="clothes__item slot">
                      <img src="/img/testinv/svg/necklace-ico.svg" alt="">
                    </div>
                  </div>
                </div>
              </div>

            </div>

            <div class="general__fast fast">
              <div class="fast__info">
                <img src="/img/testinv/svg/fast-ico.svg" alt="" class="fast__img">
                <div class="fast__text">
                  <h2 class="fast__title">
                   Fast access
                  </h2>
                  <p class="fast__descr">
                   Things that you can get quickly
                  </p>
                </div>
              </div>

              <div class="fast__slots">
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
                <div class="fast__slot">
                  <div class="fast__item slot">
                    <div class="fast__step">
                      <h3>1</h3>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="inventory__content-collateral collateral">
            <div class="collateral__content">
              <div class="collateral__info">
                <img src="/img/testinv/svg/inventory-ico.svg" alt="" class="collateral__img">

                <div class="collateral__text">
                  <h2 class="collateral__title">
                    Inventory
                  </h2>
                  <p class="collateral__descr">
                  Things you carry with you in a backpack
                  </p>
                </div>
              </div>

              <div class="collateral__weight">
                <img src="/img/testinv/svg/weight-ico.svg" alt="">

                <div id="cont" data-pct="100">
                  <svg id="svg" width="60" height="60" viewPort="0 0 100 100" version="1.1"
                    xmlns="http://www.w3.org/2000/svg">
                    <circle r="27" cx="29" cy="30" fill="transparent">
                    </circle>
                    <circle id="bar" r="27" cx="29" cy="30" fill="transparent" stroke-dasharray="105.6,76.6"
                      stroke-dashoffset="-45" stroke-linecap="round"></circle>
                      <div class="collateral__percent">
                        <h3 class="collateral__kg">50</h3>
                        <h4 class="collateral__in">kg</h4>
                      </div>
                  </svg>
                </div>
              </div>
            </div>

            <div class="collateral__slots">

              <div class="collateral__bagpack">
                <img src="/img/testinv/svg/lock-lg-ico.svg" alt="">

                <div class="collateral__error">
                  <h3>You don't have a backpack</h3>
                  <p>Buy a backpack in any affordable<br>store 24/7 or clothing store</p>
                </div>
              </div>

              <div class="collateral__slot slot slot__orange">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/jacket-ico.svg" alt="">
                  <h4 class="slot__name">Jacket</h4>
                </div>

                <div class="dropdown">
                  <div class="dropdown__content">
                    <div class="dropdown__info">
                      <div class="dropdown__img">
                        <img src="/img/testinv/svg/jacket-ico.svg" alt="">
                      </div>

                      <div class="dropdown__data">
                        <div class="dropdown__name">
                          <h3 class="dropdown__text">
                            Jacket
                          </h3>

                          <div class="dropdown__base">
                            <h4 class="dropdown__rare">
                              Rare
                            </h4>

                            <div class="dropdown__count">
                              <h5>x1</h5>
                            </div>
                          </div>
                        </div>

                        <div class="dropdown__description">
                          <h3 class="dropdown__descr">
                      Well, for example, it will be a description of a conditional
                          </h3>
                        </div>
                      </div>
                    </div>

                    <div class="dropdown__buttons">
                      <button class="btn btn--throw">
                        <svg width="26" height="27" viewBox="0 0 26 27" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M9.84062 1.05121V1.8645L5.39937 1.8645L0.95813 1.8645L0.95813 3.49109L0.95813 5.11768L13.3936 5.11768L25.8291 5.11768V3.49109V1.8645L21.3878 1.8645L16.9466 1.8645V1.05121V0.237915L13.3936 0.237915L9.84062 0.237915V1.05121ZM2.73557 15.0424C2.73668 24.3067 2.71148 23.7107 3.13473 24.4842C3.5386 25.2223 4.26186 25.7925 5.18647 26.1017L5.59368 26.2379L13.2112 26.2517C21.7461 26.2671 21.2629 26.2863 22.1065 25.8974C22.917 25.5238 23.5938 24.7993 23.9014 23.9759L24.0248 23.6455L24.0397 15.1949L24.0545 6.74426L13.3946 6.74426L2.73463 6.74426L2.73557 15.0424Z" />
                        </svg>
                        Throw it away
                      </button>
                      <button class="btn btn--share">
                        <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                        </svg>
                        Divide
                      </button>
                      <button class="btn btn--action">
                        <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                        </svg>
                        Divide
                      </button>
                    </div>
                  </div>
                </div>
              </div>

              <div class="collateral__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>

                  <div class="dropdown">
                    <div class="dropdown__content">
                      <div class="dropdown__info">
                        <div class="dropdown__img">
                          <img src="/img/testinv/svg/money-ico.svg" alt="">
                        </div>

                        <div class="dropdown__data">
                          <div class="dropdown__name">
                            <h3 class="dropdown__text">
                              Money
                            </h3>

                            <div class="dropdown__base">
                              <h4 class="dropdown__rare">
                                Rare
                              </h4>

                              <div class="dropdown__count">
                                <h5>x1</h5>
                              </div>
                            </div>
                          </div>

                          <div class="dropdown__description">
                            <h3 class="dropdown__descr">
                            Well, for example, it will be a description of a conditional
                            </h3>
                          </div>
                        </div>
                      </div>

                      <div class="dropdown__buttons">
                        <button class="btn btn--throw">
                          <svg width="26" height="27" viewBox="0 0 26 27" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M9.84062 1.05121V1.8645L5.39937 1.8645L0.95813 1.8645L0.95813 3.49109L0.95813 5.11768L13.3936 5.11768L25.8291 5.11768V3.49109V1.8645L21.3878 1.8645L16.9466 1.8645V1.05121V0.237915L13.3936 0.237915L9.84062 0.237915V1.05121ZM2.73557 15.0424C2.73668 24.3067 2.71148 23.7107 3.13473 24.4842C3.5386 25.2223 4.26186 25.7925 5.18647 26.1017L5.59368 26.2379L13.2112 26.2517C21.7461 26.2671 21.2629 26.2863 22.1065 25.8974C22.917 25.5238 23.5938 24.7993 23.9014 23.9759L24.0248 23.6455L24.0397 15.1949L24.0545 6.74426L13.3946 6.74426L2.73463 6.74426L2.73557 15.0424Z" />
                          </svg>
                          Throw it away
                        </button>
                        <button class="btn btn--share">
                          <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                          </svg>
                          Divide
                        </button>
                        <button class="btn btn--action">
                          <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                          </svg>
                          Divide
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="collateral__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>
                </div>
              </div>

              <div class="collateral__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>
                </div>
              </div>

              <div class="collateral__slot slot slot__blue">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/guns-ico.svg" alt="">
                  <h4 class="slot__name">Revolver</h4>
                </div>
              </div>

              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
              <div class="collateral__slot slot slot__lock">
                <img src="/img/testinv/svg/lock-sm-ico.svg" alt="" class="slot__locked">
              </div>
            </div>
          </div>

          <div class="inventory__content-trunk trunk">
            <div class="trunk__content">
              <div class="trunk__info">
                <img src="/img/testinv/svg/trunk-ico.svg" alt="" class="trunk__img">

                <div class="trunk__text">
                  <h2 class="trunk__title">
                    Trunk
                  </h2>
                  <p class="trunk__descr">
                 Things that you put in the trunk
                  </p>
                </div>
              </div>

              <div class="trunk__weight">
                <img src="/img/testinv/svg/weight-ico.svg" alt="">

                <div id="cont" data-pct="100">
                  <svg id="svg" width="60" height="60" viewPort="0 0 100 100" version="1.1"
                    xmlns="http://www.w3.org/2000/svg">
                    <circle r="27" cx="29" cy="30" fill="transparent">
                    </circle>
                    <circle id="bar" r="27" cx="29" cy="30" fill="transparent" stroke-dasharray="105.6,76.6"
                      stroke-dashoffset="-45" stroke-linecap="round"></circle>
                    <div class="trunk__percent">
                      <h3 class="trunk__kg">50</h3>
                      <h4 class="trunk__in">kg</h4>
                    </div>
                  </svg>
                </div>
              </div>
            </div>

            <div class="trunk__slots">

              <div class="trunk__slot slot slot__orange">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/jacket-ico.svg" alt="">
                  <h4 class="slot__name">Jacket</h4>
                </div>

                <div class="dropdown">
                  <div class="dropdown__content">
                    <div class="dropdown__info">
                      <div class="dropdown__img">
                        <img src="/img/testinv/svg/jacket-ico.svg" alt="">
                      </div>

                      <div class="dropdown__data">
                        <div class="dropdown__name">
                          <h3 class="dropdown__text">
                            Jacket
                          </h3>

                          <div class="dropdown__base">
                            <h4 class="dropdown__rare">
                              Rare
                            </h4>

                            <div class="dropdown__count">
                              <h5>x1</h5>
                            </div>
                          </div>
                        </div>

                        <div class="dropdown__description">
                          <h3 class="dropdown__descr">
                          Well, for example, it will be a description of a conditional
                          </h3>
                        </div>
                      </div>
                    </div>

                    <div class="dropdown__buttons">
                      <button class="btn btn--throw">
                        <svg width="26" height="27" viewBox="0 0 26 27" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M9.84062 1.05121V1.8645L5.39937 1.8645L0.95813 1.8645L0.95813 3.49109L0.95813 5.11768L13.3936 5.11768L25.8291 5.11768V3.49109V1.8645L21.3878 1.8645L16.9466 1.8645V1.05121V0.237915L13.3936 0.237915L9.84062 0.237915V1.05121ZM2.73557 15.0424C2.73668 24.3067 2.71148 23.7107 3.13473 24.4842C3.5386 25.2223 4.26186 25.7925 5.18647 26.1017L5.59368 26.2379L13.2112 26.2517C21.7461 26.2671 21.2629 26.2863 22.1065 25.8974C22.917 25.5238 23.5938 24.7993 23.9014 23.9759L24.0248 23.6455L24.0397 15.1949L24.0545 6.74426L13.3946 6.74426L2.73463 6.74426L2.73557 15.0424Z" />
                        </svg>
                        Throw it away
                      </button>
                      <button class="btn btn--share">
                        <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                        </svg>
                        Divide
                      </button>
                      <button class="btn btn--action">
                        <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                        </svg>
                        Divide
                      </button>
                    </div>
                  </div>
                </div>
              </div>

              <div class="trunk__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>

                  <div class="dropdown">
                    <div class="dropdown__content">
                      <div class="dropdown__info">
                        <div class="dropdown__img">
                          <img src="/img/testinv/svg/money-ico.svg" alt="">
                        </div>

                        <div class="dropdown__data">
                          <div class="dropdown__name">
                            <h3 class="dropdown__text">
                              Money
                            </h3>

                            <div class="dropdown__base">
                              <h4 class="dropdown__rare">
                                Rare
                              </h4>

                              <div class="dropdown__count">
                                <h5>x1</h5>
                              </div>
                            </div>
                          </div>

                          <div class="dropdown__description">
                            <h3 class="dropdown__descr">
                          Well, for example, it will be a description of a conditional
                            </h3>
                          </div>
                        </div>
                      </div>

                      <div class="dropdown__buttons">
                        <button class="btn btn--throw">
                          <svg width="26" height="27" viewBox="0 0 26 27" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M9.84062 1.05121V1.8645L5.39937 1.8645L0.95813 1.8645L0.95813 3.49109L0.95813 5.11768L13.3936 5.11768L25.8291 5.11768V3.49109V1.8645L21.3878 1.8645L16.9466 1.8645V1.05121V0.237915L13.3936 0.237915L9.84062 0.237915V1.05121ZM2.73557 15.0424C2.73668 24.3067 2.71148 23.7107 3.13473 24.4842C3.5386 25.2223 4.26186 25.7925 5.18647 26.1017L5.59368 26.2379L13.2112 26.2517C21.7461 26.2671 21.2629 26.2863 22.1065 25.8974C22.917 25.5238 23.5938 24.7993 23.9014 23.9759L24.0248 23.6455L24.0397 15.1949L24.0545 6.74426L13.3946 6.74426L2.73463 6.74426L2.73557 15.0424Z" />
                          </svg>
                          Throw it away
                        </button>
                        <button class="btn btn--share">
                          <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                          </svg>
                          Divide
                        </button>
                        <button class="btn btn--action">
                          <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                          </svg>
                          Divide
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="trunk__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>
                </div>
              </div>

              <div class="trunk__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>
                </div>
              </div>

              <div class="trunk__slot slot slot__blue">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/guns-ico.svg" alt="">
                  <h4 class="slot__name">Revolver</h4>
                </div>
              </div>

              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>
              <div class="collateral__slot slot"></div>

            </div>
          </div>

          <div class="inventory__content-environment environment none">
            <div class="environment__content">
              <div class="environment__info">
                <img src="/img/testinv/svg/environment-ico.svg" alt="" class="environment__img">

                <div class="environment__text">
                  <h2 class="environment__title">
                    Environment
                  </h2>
                  <p class="environment__descr">
              Things that are next to you
                  </p>
                </div>
              </div>
            </div>

            <div class="environment__slots">

              <div class="environment__slot slot slot__orange">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/jacket-ico.svg" alt="">
                  <h4 class="slot__name">Jacket</h4>
                </div>

                <div class="dropdown">
                  <div class="dropdown__content">
                    <div class="dropdown__info">
                      <div class="dropdown__img">
                        <img src="/img/testinv/svg/jacket-ico.svg" alt="">
                      </div>

                      <div class="dropdown__data">
                        <div class="dropdown__name">
                          <h3 class="dropdown__text">
                            Jacket
                          </h3>

                          <div class="dropdown__base">
                            <h4 class="dropdown__rare">
                              Rare
                            </h4>

                            <div class="dropdown__count">
                              <h5>x1</h5>
                            </div>
                          </div>
                        </div>

                        <div class="dropdown__description">
                          <h3 class="dropdown__descr">
                            Well, for example, it will be a description of a conditional
                          </h3>
                        </div>
                      </div>
                    </div>

                    <div class="dropdown__buttons">
                      <button class="btn btn--throw">
                        <svg width="26" height="27" viewBox="0 0 26 27" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M9.84062 1.05121V1.8645L5.39937 1.8645L0.95813 1.8645L0.95813 3.49109L0.95813 5.11768L13.3936 5.11768L25.8291 5.11768V3.49109V1.8645L21.3878 1.8645L16.9466 1.8645V1.05121V0.237915L13.3936 0.237915L9.84062 0.237915V1.05121ZM2.73557 15.0424C2.73668 24.3067 2.71148 23.7107 3.13473 24.4842C3.5386 25.2223 4.26186 25.7925 5.18647 26.1017L5.59368 26.2379L13.2112 26.2517C21.7461 26.2671 21.2629 26.2863 22.1065 25.8974C22.917 25.5238 23.5938 24.7993 23.9014 23.9759L24.0248 23.6455L24.0397 15.1949L24.0545 6.74426L13.3946 6.74426L2.73463 6.74426L2.73557 15.0424Z" />
                        </svg>
                        Throw it away
                      </button>
                      <button class="btn btn--share">
                        <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                        </svg>
                        Divide
                      </button>
                      <button class="btn btn--action">
                        <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                          <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                        </svg>
                        Divide
                      </button>
                    </div>
                  </div>
                </div>
              </div>

              <div class="environment__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>

                  <div class="dropdown">
                    <div class="dropdown__content">
                      <div class="dropdown__info">
                        <div class="dropdown__img">
                          <img src="/img/testinv/svg/money-ico.svg" alt="">
                        </div>

                        <div class="dropdown__data">
                          <div class="dropdown__name">
                            <h3 class="dropdown__text">
                              Money
                            </h3>

                            <div class="dropdown__base">
                              <h4 class="dropdown__rare">
                                Rare
                              </h4>

                              <div class="dropdown__count">
                                <h5>x1</h5>
                              </div>
                            </div>
                          </div>

                          <div class="dropdown__description">
                            <h3 class="dropdown__descr">
                          Well, for example, it will be a description of a conditional
                            </h3>
                          </div>
                        </div>
                      </div>

                      <div class="dropdown__buttons">
                        <button class="btn btn--throw">
                          <svg width="26" height="27" viewBox="0 0 26 27" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M9.84062 1.05121V1.8645L5.39937 1.8645L0.95813 1.8645L0.95813 3.49109L0.95813 5.11768L13.3936 5.11768L25.8291 5.11768V3.49109V1.8645L21.3878 1.8645L16.9466 1.8645V1.05121V0.237915L13.3936 0.237915L9.84062 0.237915V1.05121ZM2.73557 15.0424C2.73668 24.3067 2.71148 23.7107 3.13473 24.4842C3.5386 25.2223 4.26186 25.7925 5.18647 26.1017L5.59368 26.2379L13.2112 26.2517C21.7461 26.2671 21.2629 26.2863 22.1065 25.8974C22.917 25.5238 23.5938 24.7993 23.9014 23.9759L24.0248 23.6455L24.0397 15.1949L24.0545 6.74426L13.3946 6.74426L2.73463 6.74426L2.73557 15.0424Z" />
                          </svg>
                          Throw it away
                        </button>
                        <button class="btn btn--share">
                          <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                          </svg>
                          Divide
                        </button>
                        <button class="btn btn--action">
                          <svg width="25" height="24" viewBox="0 0 25 24" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                              d="M1.36532 0.053022C0.827458 0.220192 0.4436 0.541199 0.20833 1.0206L0.0585327 1.32574L0.0585327 12.0056L0.0585327 22.6855L0.20833 22.9907C0.398637 23.3785 0.699295 23.6701 1.09912 23.8547L1.41372 24H5.72231H10.0309L10.3528 23.8463C10.7515 23.6559 11.1149 23.285 11.2717 22.9084L11.384 22.6386L11.384 12.0056L11.384 1.37268L11.2717 1.10289C11.1149 0.726349 10.7515 0.355392 10.3528 0.164985L10.031 0.0112882L5.79495 0.00204011C2.46162 -0.00518937 1.51768 0.00565485 1.36532 0.053022ZM14.7928 0.0514728C14.2781 0.187284 13.7789 0.619832 13.5767 1.105C13.4678 1.36649 13.4652 1.41024 13.4652 2.99227C13.4652 4.57764 13.4676 4.61754 13.5776 4.88165C13.7343 5.25824 14.0978 5.62915 14.4965 5.81956L14.8184 5.97326L19.127 5.97326L23.4356 5.97326L23.7578 5.82515C24.1582 5.64117 24.446 5.3604 24.6409 4.96395L24.7907 4.65881V2.99227V1.32574L24.6409 1.0206C24.446 0.62415 24.1582 0.343375 23.7578 0.159399L23.4356 0.0112882L19.2248 0.00222789C15.9947 -0.00476687 14.9625 0.00673457 14.7928 0.0514728ZM14.7928 9.06484C14.2781 9.20066 13.7789 9.6332 13.5767 10.1184C13.4678 10.3799 13.4652 10.4236 13.4652 12.0056C13.4652 13.591 13.4676 13.6309 13.5776 13.895C13.7343 14.2716 14.0978 14.6425 14.4965 14.8329L14.8184 14.9866L19.127 14.9866H23.4356L23.7578 14.8385C24.1582 14.6545 24.446 14.3738 24.6409 13.9773L24.7907 13.6722V12.0056V10.3391L24.6409 10.034C24.446 9.63752 24.1582 9.35675 23.7578 9.17277L23.4356 9.02466L19.2248 9.0156C15.9947 9.0086 14.9625 9.02011 14.7928 9.06484ZM14.7928 18.0782C14.2781 18.214 13.7789 18.6466 13.5767 19.1317C13.4678 19.3932 13.4652 19.437 13.4652 21.019C13.4652 22.6044 13.4676 22.6443 13.5776 22.9084C13.7343 23.285 14.0978 23.6559 14.4965 23.8463L14.8184 24H19.127H23.4356L23.7578 23.8519C24.1582 23.6679 24.446 23.3871 24.6409 22.9907L24.7907 22.6855V21.019V19.3525L24.6409 19.0473C24.446 18.6509 24.1582 18.3701 23.7578 18.1861L23.4356 18.038L19.2248 18.029C15.9947 18.022 14.9625 18.0335 14.7928 18.0782Z" />
                          </svg>
                          Divide
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="environment__slot slot slot__green">
                <h3 class="slot__weight">0,5 kg</h3>
                <div class="slot__info">
                  <img src="/img/testinv/svg/money-ico.svg" alt="">
                  <h4 class="slot__name">Money</h4>
                </div>
              </div>

              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>
              <div class="environment__slot slot"></div>

            </div>
          </div>

        </div>

        <div class="inventory__ditch">
          <a>
            <img src="/img/testinv/svg/ditch-ico.svg" alt="" class="inventory__ditch_ico">
            <div class="inventory__ditch_text">
              <h3>Throw it away<br>Inventory item</h3>
            </div>
          </a>
        </div>
      </div>
    </div>

  </main>
  </div>
</body>
</template>

<script>
export default {
  name: 'TestInventory',
  data() {
    return {
      zoomDesign: 1,
    }
  },
  mounted(){
    this.handleResize();
    window.addEventListener('resize', this.handleResize);
  },
  beforeDestroy(){
    window.removeEventListener('resize', this.handleResize);
  },
  methods: {
    handleResize() {
        let zoomCountOne = document.body.clientWidth / 1920;//1920;
        let zoomCountTwo = document.body.clientHeight / 1080;//1080;

        if(zoomCountOne < zoomCountTwo) this.zoomDesign = zoomCountOne;
        else this.zoomDesign = zoomCountTwo;
    }
  }
}
</script>

<style lang="scss" scoped>
body {
  margin: 0;
  font-family: 'Akrobat';
  overflow-anchor: none;
  background-image: url('/img/testinv/bg-home.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  width: 100vw;
  height: 100vh;
  color: #fff;
}

ul {
  list-style-type: none;
}

.btn {
  cursor: pointer;
  transition: .7s all;
  padding: 0;
  background: transparent;
  border: none;
  color: #fff;

  &--throw {
    color: rgba(255, 255, 255, 0.25);
    width: 81px;
    height: 75.16999999999999px;
    display: flex;
    align-items: center;
    flex-direction: column;
    background: rgba(255, 255, 255, 0.03);
    border: 1px solid rgba(255, 255, 255, 0.1);
    padding: 20px 7.71px 7.24px 9.68px;
    font-weight: 500;
    font-size: 10.6008px;
    line-height: 13px;
    transition: .7s all;

    svg {
      margin-bottom: 10px;
      fill: rgba(255, 255, 255, 0.25);
      transition: .7s all;
    }

    &:hover {
      color: #fff;
      background: rgba(255, 255, 255, 0.1);
      border: 1px solid rgba(255, 255, 255, 0.25);

      svg {
        fill: #fff;
      }
    }
  }

  &--share {
    color: rgba(255, 255, 255, 0.25);
    width: 81px;
    height: 75.16999999999999px;
    display: flex;
    align-items: center;
    flex-direction: column;
    background: rgba(255, 255, 255, 0.03);
    border: 1px solid rgba(255, 255, 255, 0.1);
    padding: 20px 7.71px 7.24px 9.68px;
    font-weight: 500;
    font-size: 10.6008px;
    line-height: 13px;
    transition: .7s all;

    svg {
      margin-bottom: 10px;
      fill: rgba(255, 255, 255, 0.25);
      transition: .7s all;
    }

    &:hover {
      color: #fff;
      background: rgba(255, 255, 255, 0.1);
      border: 1px solid rgba(255, 255, 255, 0.25);

      svg {
        fill: #fff;
      }
    }
  }

  &--action {
    color: rgba(255, 255, 255, 0.25);
    width: 81px;
    height: 75.16999999999999px;
    display: flex;
    align-items: center;
    flex-direction: column;
    background: rgba(255, 255, 255, 0.03);
    border: 1px solid rgba(255, 255, 255, 0.1);
    padding: 20px 7.71px 7.24px 9.68px;
    font-weight: 500;
    font-size: 10.6008px;
    line-height: 13px;
    transition: .7s all;

    svg {
      margin-bottom: 10px;
      fill: rgba(255, 255, 255, 0.25);
      transition: .7s all;
    }

    &:hover {
      color: #fff;
      background: rgba(255, 255, 255, 0.1);
      border: 1px solid rgba(255, 255, 255, 0.25);

      svg {
        fill: #fff;
      }
    }
  }
}

.container {
  max-width: 1667px;
  margin: 0 auto;
}

.inventory {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  &__content {
    display: flex;
  }

  &__title {
    margin-left: 20px;
    margin-top: 33px;
    margin-bottom: 22px;
    max-width: 196px;
    font-weight: 800;
    font-size: 43.408699999999996px;
    line-height: 52px;
    color: #FFFFFF;
    text-transform: uppercase;
    text-shadow: 0 0 113px rgba(255, 255, 255, 0.25);
    position: relative;

    &::after {
      content: '';
      position: absolute;
      width: 294px;
      height: 78px;
      left: 50%;
      top: 50%;
      transform: translate(-50%, -50%);
      background-image: url('/img/testinv/inventory-text.png');
      background-repeat: no-repeat;
      background-size: contain;
      background-position: center center;
    }
  }

  &__ditch {
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 35px 0;
    margin-top: 19px;
    max-height: 105px;
    background: linear-gradient(90deg, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.17) 47.5%, rgba(0, 0, 0, 0) 100%);

    a {
      cursor: pointer;
      display: flex;
      justify-content: center;
      align-items: center;
    }

    &_ico {
      width: 23px;
      height: 30px;
      opacity: 20%;
      margin-right: 14px;
    }

    &_text {

      h3 {
        color: rgba(#fff, 0.3);
        font-weight: 600;
        font-size: 15px;
        line-height: 18px;
        text-transform: uppercase;
      }
    }
  }
}

.general {
  width: 496.13px;
  margin-right: 80px;

  &__block {
    display: flex;
  }
}

.gun {

  &__info {
    display: flex;
    align-items: baseline;
    margin-bottom: 22px;
    margin-left: 20px;
  }

  &__img {
    margin-right: 13px;

    img {
      width: 18.999999999999996px;
      height: 20px;
    }
  }

  &__title {
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #301934 ;
  }

  &__descr {
    max-width: 104px;
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }

  &__slot {
    display: flex;
    align-items: center;
  }

  &__slots {
    display: grid;
    grid-template-rows: 80px 80px 80px 80px;
    row-gap: 15px;
    max-width: 113px;
  }

  &__step {
    width: 20px;
    height: 20px;
    border: 2.8459999999999996px solid #301934 ;
    border-radius: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-right: 13px;

    h3 {
      color: #fff;
      font-weight: 700;
      font-size: 12px;
      line-height: 14px;
    }
  }
}

.clothes {
  margin-left: 66px;

  &__slots {
    display: grid;
    grid-template-columns: 80px 80px 80px;
    grid-gap: 15px;
  }

  &__item {
    display: flex;
    align-items: center;
    justify-content: center;

    img {
      width: 35.709998219999996px;
      height: 37.52400078px;
    }
  }

  &__info {
    display: flex;
    align-items: baseline;
    margin-bottom: 22px;
  }

  &__img {
    margin-right: 13px;

    img {
      width: 13px;
      height: 17px;
    }
  }

  &__title {
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #301934 ;
  }

  &__descr {
    max-width: 159px;
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }
}

.fast {
  margin-top: 55px;
  margin-bottom: 34px;

  &__info {
    display: flex;
    align-items: center;
    flex-direction: row;
    margin-bottom: 22px;

    img {
      width: 23px;
      height: 23px;
    }
  }

  &__text {
    display: flex;
    align-items: center;
    flex-direction: row;
  }

  &__title {
    margin-left: 12px;
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #301934 ;
  }

  &__descr {
    margin-left: 21px;
    max-width: 120px;
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }

  &__slots {
    display: grid;
    grid-template-columns: 80px 80px 80px 80px 80px;
    grid-gap: 15px;
  }

  &__step {
    width: 20px;
    height: 20px;
    border: 2.8459999999999996px solid #301934 ;
    border-radius: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 6px 6px 0;

    h3 {
      color: #fff;
      font-weight: 700;
      font-size: 12px;
      line-height: 14px;
    }
  }

  &__item {
    display: flex;
    align-items: flex-end;
    justify-content: flex-end;
  }
}

.environment {
  width: 272px;

  &__content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 15px;
  }

  &__info {
    display: flex;
    align-items: baseline;
    margin-bottom: 22px;

    img {
      width: 22px;
      height: 22px;
      margin-right: 12px;
    }
  }

  &__title {
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #301934 ;
  }

  &__descr {
    max-width: 130px;
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }

  &__percent {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  &__kg {
    font-weight: 600;
    font-size: 16px;
    line-height: 18.999999999999996px;
  }

  &__in {
    font-weight: 600;
    font-size: 16px;
    line-height: 18.999999999999996px;
  }

  &__weight {
    display: flex;
    align-items: center;

    img {
      width: 25px;
      height: 25px;
    }
  }

  &__slots {
    display: grid;
    grid-template-columns: 80px 80px 80px;
    grid-gap: 15px;
  }
}

.trunk {
  width: 460px;

  &__content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 15px;
  }

  &__info {
    display: flex;
    align-items: baseline;
    margin-bottom: 22px;

    img {
      width: 29px;
      height: 16px;
      margin-right: 12px;
    }
  }

  &__title {
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #301934 ;
  }

  &__descr {
    max-width: 130px;
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }

  &__percent {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  &__kg {
    font-weight: 600;
    font-size: 16px;
    line-height: 18.999999999999996px;
  }

  &__in {
    font-weight: 600;
    font-size: 16px;
    line-height: 18.999999999999996px;
  }

  &__weight {
    display: flex;
    align-items: center;

    img {
      width: 25px;
      height: 25px;
    }
  }

  &__slots {
    display: grid;
    grid-template-columns: 80px 80px 80px 80px 80px;
    grid-gap: 15px;
  }
}

.collateral {
  width: 555px;
  margin-right: 45px;
  position: relative;

  &__bagpack {
    position: absolute;
    top: 50%;
    left: 50%;
    z-index: 9999;
    transform: translate(-50%, -50%);
    display: flex;
    align-items: center;

    img {
      width: 29px;
      height: 39px;
    }
  }

  &__error {
    margin-left: 12px;

    h3 {
      font-weight: 700;
      font-size: 20px;
      line-height: 24px;
      text-transform: uppercase;
      color: #301934 ;
    }

    p {
      max-width: 186px;
      font-weight: 600;
      font-size: 12px;
      line-height: 14px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
    }
  }

  &__content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 15px;
  }

  &__info {
    display: flex;
    align-items: baseline;
    margin-bottom: 22px;

    img {
      width: 18px;
      height: 21px;
      margin-right: 12px;
    }
  }

  &__title {
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #301934 ;
  }

  &__descr {
    max-width: 130px;
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }

  &__percent {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  &__kg {
    font-weight: 600;
    font-size: 16px;
    line-height: 18.999999999999996px;
  }

  &__in {
    font-weight: 600;
    font-size: 16px;
    line-height: 18.999999999999996px;
  }

  &__weight {
    display: flex;
    align-items: center;

    img {
      width: 25px;
      height: 25px;
    }
  }

  &__slots {
    display: grid;
    grid-template-columns: 80px 80px 80px 80px 80px 80px;
    grid-gap: 15px;
  }
}

.slot {
  width: 80px;
  height: 80px;
  background: radial-gradient(50% 50% at 50% 50%, rgba(0, 0, 0, 0.05) 0%, rgba(0, 0, 0, 0.09) 100%);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 2px;
  transition: border .7s;
  position: relative;

  &:hover {
    cursor: pointer;
    border: 1px solid #fff;
  }

  &__lock {
    pointer-events: none;
  }

  &__locked {
    position: absolute;
    top: -8px;
    right: 8px;
    width: 11px;
    height: 15px;
  }

  &__weight {
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    margin: 5px 0 0 8px;
  }

  &__info {
    text-align: center;

    img {
      display: block;
      margin: 0 auto;
      height: 37.99999999999999px;
      width: 57.6299576px;
    }
  }

  &__name {
    font-weight: 600;
    font-size: 12px;
    line-height: 14px;
    text-align: center;
  }

  &__orange {
    background: radial-gradient(50% 50% at 50% 50%, rgba(255, 127, 55, 0.0275) 0%, rgba(255, 127, 55, 0.088) 100%);
    border: 1px solid rgba(255, 127, 55, 0.55);
  }

  &__green {
    background: radial-gradient(50% 50% at 50% 50%, rgba(92, 255, 128, 0.0275) 0%, rgba(92, 255, 128, 0.088) 100%);
    border: 1px solid #5CFF80;
  }

  &__blue {
    background: radial-gradient(50% 50% at 50% 50%, rgba(115, 92, 255, 0.0275) 0%, rgba(115, 92, 255, 0.088) 100%);
    border: 1px solid #735CFF;
  }
}

.dropdown {
  position: relative;
  top: 0;
  height: 100%;
  left: -1%;
  margin-top: -93%;
  height: 78px;

  &__content {
    opacity: 0;
    visibility: hidden;
    display: flex;
    flex-direction: column;
    position: absolute;
    top: 108%;
    z-index: 2;
    transition: .7s all;
  }

  &__info {
    display: flex;
    background: #181820;
    margin-bottom: 5px;
  }

  &__img {
    width: 75px;
    height: 75px;
    background: rgba(255, 255, 255, 0.03);
    padding: 17px 6px 16px 10px;

    img {
      display: block;
      width: 59px;
      height: 42px;
    }
  }

  &__data {
    display: flex;
    flex-direction: column;
    width: 100%;
  }

  &__name {
    display: flex;
    align-items: center;
    justify-content: space-between;
    background: linear-gradient(90.02deg, rgba(255, 255, 255, 0.02) 0.02%, rgba(255, 255, 255, 0) 99.98%);
    padding: 3px 8px 3px 11px;
  }

  &__text {
    font-weight: 500;
    font-size: 15.60119866px;
    line-height: 16px;
  }

  &__base {
    display: flex;
    align-items: center;
  }

  &__rare {
    color: #FF00D6;
    font-weight: 700;
    font-size: 16.8799px;
    line-height: 16px;
    margin-right: 9px;
  }

  &__count {
    width: 20px;
    height: 20px;
    display: flex;
    align-items: center;
    justify-content: center;
    background: rgba(71, 44, 132, 0.05);
    border: 1px solid #301934 ;
    padding: 4px;

    h5 {
      font-size: 13px;
      line-height: 13px;
    }
  }

  &__descr {
    font-weight: 400;
    font-size: 14px;
    line-height: 15px;
    color: rgba(255, 255, 255, 0.25);
    max-width: 148px;
  }

  &__description {
    padding: 6px 16px 13px 16px;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  &__buttons {
    display: flex;
    gap: 7.25px;
  }
}

#svg {
  width: 60px;
  height: 60px;
  position: relative;
}

#svg circle {
  stroke-dashoffset: 0;
  stroke: rgba(#fff, 0.25);
  stroke-width: 4px;
}

#svg #bar {
  stroke-miterlimit: 5;
  stroke-dashoffset: 200;
  stroke: #5CFF80;
}

#cont {
  margin-left: 15px;
  display: block;
  height: 60px;
  width: 60px;
  border-radius: 100%;
  position: relative;
}

.dropdown:hover .dropdown__content {
  opacity: 1;
  visibility: visible;
}

.none {
  display: none;
}

@media (min-height: 1350px) {
  .container {
    padding: 0 35px;
  }
}
</style>
