<template>
  <div class="finder-tab">
    <div class="finder-tab__orb finder-tab__orb--one"></div>
    <div class="finder-tab__orb finder-tab__orb--two"></div>

    <div class="finder-tab__top">
      <div class="finder-brand">
        <img src="/img/hud/smartphone/desktopTab/apps/finder.svg" alt="Finder">
        <div>
          <div class="finder-brand__name">Finder</div>
          <div class="finder-brand__caption">RP dating preview</div>
        </div>
      </div>
      <button class="finder-tab__settings" @click="activeScreen = 'setup'"></button>
    </div>

    <transition name="finder-fade" mode="out-in">
      <section v-if="activeScreen === 'welcome'" key="welcome" class="finder-screen finder-welcome">
        <div class="finder-welcome__logo">
          <img src="/img/hud/smartphone/desktopTab/apps/finder.svg" alt="Finder">
        </div>
        <div class="finder-kicker">Welcome to Finder</div>
        <h2>Meet people around Los Santos.</h2>
        <p>
          Create a clean RP dating profile, discover nearby characters, and start conversations
          after a mutual match.
        </p>
        <button class="finder-primary" @click="activeScreen = 'setup'">Create profile</button>
        <button class="finder-ghost" @click="activeScreen = 'discover'">Preview discover</button>
      </section>

      <section v-else-if="activeScreen === 'setup'" key="setup" class="finder-screen finder-setup">
        <div class="finder-section-title">
          <span>Profile setup</span>
          <small>TODO: connect Finder profile save to backend later</small>
        </div>
        <label>
          Display name
          <input v-model="profile.name" type="text">
        </label>
        <label>
          Age
          <input v-model="profile.age" type="text">
        </label>
        <label>
          About
          <textarea v-model="profile.bio"></textarea>
        </label>
        <div class="finder-profile-source">
          <span>Character</span>
          <strong>{{ currentCharacterLabel }}</strong>
        </div>
        <div class="finder-setup__chips">
          <span v-for="tag in profile.tags" :key="tag">{{ tag }}</span>
        </div>
        <button class="finder-primary" @click="saveProfile">Save profile</button>
      </section>

      <section v-else-if="activeScreen === 'discover'" key="discover" class="finder-screen finder-discover">
        <div class="finder-section-title finder-section-title--inline">
          <div>
            <span>Discover</span>
          <small>{{ dataSourceCaption }}</small>
          </div>
          <em>{{ activeIndex + 1 }}/{{ profileCards.length }}</em>
        </div>

        <div class="finder-card-stack">
          <div v-if="nextCard" class="finder-card finder-card--next">
            <div class="finder-card__photo" :style="profileImageStyle(nextCard)"></div>
          </div>

          <div
            v-if="currentCard"
            class="finder-card finder-card--active"
            :class="{ 'is-dragging': isDragging, 'is-leaving': leavingDirection }"
            :style="cardStyle"
            @mousedown="startDrag"
            @touchstart="startDrag"
          >
            <div class="finder-card__stamp finder-card__stamp--like" :style="{ opacity: likeOpacity }">LIKE</div>
            <div class="finder-card__stamp finder-card__stamp--pass" :style="{ opacity: passOpacity }">PASS</div>
            <div class="finder-card__photo" :style="profileImageStyle(currentCard)">
              <div v-if="currentCard.online" class="finder-card__badge">Online now</div>
            </div>
            <div class="finder-card__body">
              <div class="finder-card__name">
                {{ currentCard.displayName }}
                <span v-if="currentCard.age">{{ currentCard.age }}</span>
              </div>
              <div class="finder-card__meta">
                {{ currentCard.distanceLabel }} away - {{ currentCard.headline }}
              </div>
              <p>{{ currentCard.bio }}</p>
              <div class="finder-card__tags">
                <span v-for="tag in currentCard.tags" :key="tag">{{ tag }}</span>
              </div>
            </div>
          </div>

          <div v-else class="finder-empty-card">
            <strong>No profiles nearby</strong>
            <span>{{ finderState.error || 'No active Finder profiles found.' }}</span>
          </div>
        </div>

        <div class="finder-actions">
          <button class="finder-action finder-action--pass" @click="completeSwipe('pass')">x</button>
          <button class="finder-action finder-action--like" @click="completeSwipe('like')">heart</button>
        </div>
      </section>

      <section v-else-if="activeScreen === 'matches'" key="matches" class="finder-screen finder-matches">
        <div class="finder-section-title">
          <span>Matches</span>
          <small>TODO: real mutual matches</small>
        </div>
        <button
          v-for="match in demoMatches"
          :key="match.id"
          class="finder-match"
          @click="openChat(match)"
        >
          <div class="finder-match__avatar" :style="profileImageStyle(match)"></div>
          <div>
            <strong>{{ match.displayName }}</strong>
            <span>{{ match.last }}</span>
          </div>
        </button>
      </section>

      <section v-else key="chat" class="finder-screen finder-chat">
        <div class="finder-chat__head">
          <button @click="activeScreen = 'matches'"></button>
          <div>
            <strong>{{ activeMatch.displayName }}</strong>
            <span>Matched today</span>
          </div>
        </div>
        <div class="finder-chat__body">
          <div class="bubble bubble--them">Hey, want to meet at the pier?</div>
          <div class="bubble bubble--me">Could be a fun RP scene.</div>
          <div class="finder-chat__todo">TODO: real Finder messages after backend phase</div>
        </div>
        <div class="finder-chat__input">
          <span>Message...</span>
          <button>Send</button>
        </div>
      </section>
    </transition>

    <nav class="finder-nav">
      <button :class="{ active: activeScreen === 'discover' }" @click="activeScreen = 'discover'">Cards</button>
      <button :class="{ active: activeScreen === 'matches' || activeScreen === 'chat' }" @click="activeScreen = 'matches'">Matches</button>
      <button :class="{ active: activeScreen === 'setup' }" @click="activeScreen = 'setup'">Profile</button>
    </nav>
  </div>
</template>

<script>
const SWIPE_THRESHOLD = 72
const SWIPE_EXIT = 360

const fallbackGradient = 'linear-gradient(135deg, #ff136f 0%, #ff743d 52%, #20111d 100%)'

// Finder profile data contract for the backend phase:
// {
//   id: Number|String, characterUuid: Number|String, firstName: String, lastName: String,
//   displayName: String, gender: 'male'|'female'|'other'|null, age: Number|null,
//   avatarUrl: String|null, level: Number|null, phoneNumber: Number|String|null,
//   online: Boolean, distanceMeters: Number|null, headline: String, bio: String, tags: String[]
// }
export default {
  name: 'FinderTab',

  data() {
    return {
      activeScreen: 'welcome',
      activeIndex: 0,
      activeMatch: { displayName: 'Maya Stone' },
      profile: {
        name: 'Your Character',
        age: '24',
        bio: 'Looking for clean RP stories, late-night drives, and coffee scenes.',
        tags: ['Roleplay', 'Cruising', 'Coffee']
      },
      drag: {
        active: false,
        startX: 0,
        startY: 0,
        x: 0,
        y: 0
      },
      leavingDirection: null,
      // TODO: replace fallback profiles with server-loaded active Finder profiles.
      // These follow the final Finder profile contract so backend wiring can swap them directly.
      fallbackProfiles: [
        {
          id: 'demo-1',
          characterUuid: null,
          firstName: 'Maya',
          lastName: 'Stone',
          displayName: 'Maya Stone',
          gender: 'female',
          age: 23,
          avatarUrl: null,
          gradient: 'linear-gradient(135deg, #ff2f7d 0%, #ff9367 48%, #23111f 100%)',
          level: null,
          phoneNumber: null,
          online: true,
          distanceMeters: 1400,
          headline: 'Designer',
          bio: 'Ocean drives, night photos, and people who can improvise a good scene.',
          tags: ['Social', 'Fashion', 'Nightlife']
        },
        {
          id: 'demo-2',
          characterUuid: null,
          firstName: 'Alex',
          lastName: 'Reed',
          displayName: 'Alex Reed',
          gender: 'male',
          age: 27,
          avatarUrl: null,
          gradient: 'linear-gradient(135deg, #24d2ff 0%, #8058ff 48%, #160f24 100%)',
          level: null,
          phoneNumber: null,
          online: false,
          distanceMeters: 2100,
          headline: 'Mechanic',
          bio: 'Cars, diner stops, and stories that do not end after one scene.',
          tags: ['Cars', 'Chill', 'RP']
        }
      ],
      // TODO: replace with mutual matches returned by Finder backend.
      demoMatches: [
        {
          id: 'match-demo-1',
          displayName: 'Maya Stone',
          last: 'New match - say hello',
          avatarUrl: null,
          gradient: 'linear-gradient(135deg, #ff2f7d 0%, #ff9367 48%, #23111f 100%)'
        },
        {
          id: 'match-demo-2',
          displayName: 'Alex Reed',
          last: 'Liked your profile',
          avatarUrl: null,
          gradient: 'linear-gradient(135deg, #24d2ff 0%, #8058ff 48%, #160f24 100%)'
        }
      ]
    }
  },

  computed: {
    finderState() {
      return this.$store.state.smartphone.finderPage
    },

    existingCharacterData() {
      const stats = this.$store.state.optionsMenu && this.$store.state.optionsMenu.statistics
      const phoneConfig = this.$store.state.smartphone && this.$store.state.smartphone.configuration

      return {
        characterUuid: stats && (stats.passportNumber || stats.uuid || null),
        displayName: (phoneConfig && phoneConfig.Username) || (stats && stats.username) || '',
        gender: stats && stats.maritalStatus ? stats.maritalStatus.sex : null,
        level: stats ? stats.level : null,
        phoneNumber: (phoneConfig && phoneConfig.Number) || (stats && stats.phoneNumber) || null,
        online: true,
        family: stats ? stats.family : null,
        organization: stats ? stats.organization : null
      }
    },

    currentCharacterLabel() {
      const data = this.existingCharacterData
      const parts = [data.displayName || 'Unknown character']
      if (data.phoneNumber) parts.push(`#${data.phoneNumber}`)
      if (data.level) parts.push(`Level ${data.level}`)
      return parts.join(' - ')
    },

    serverProfiles() {
      return this.finderState.profiles || []
    },

    profileCards() {
      const source = this.finderState.isProfilesLoaded ? this.serverProfiles : this.fallbackProfiles
      return source.map(this.normalizeProfile)
    },

    dataSourceCaption() {
      if (!this.finderState.profile && this.finderState.isProfileLoaded) return 'Create profile to start discovering'
      return this.finderState.isProfilesLoaded ? 'Real active Finder profiles' : 'Loading Finder profiles'
    },

    currentCard() {
      if (!this.profileCards.length) return null
      return this.profileCards[this.activeIndex % this.profileCards.length]
    },

    nextCard() {
      if (this.profileCards.length < 2) return null
      return this.profileCards[(this.activeIndex + 1) % this.profileCards.length]
    },

    isDragging() {
      return this.drag.active
    },

    dragPower() {
      return Math.min(Math.abs(this.drag.x) / SWIPE_THRESHOLD, 1)
    },

    likeOpacity() {
      return this.drag.x > 0 ? this.dragPower : 0
    },

    passOpacity() {
      return this.drag.x < 0 ? this.dragPower : 0
    },

    cardStyle() {
      const rotate = this.drag.x / 18
      const scale = this.drag.active ? 1.015 : 1
      return {
        transform: `translate3d(${this.drag.x}px, ${this.drag.y}px, 0) rotate(${rotate}deg) scale(${scale})`,
        transition: this.drag.active ? 'none' : 'transform .32s cubic-bezier(.2, .9, .2, 1), opacity .24s ease'
      }
    }
  },

  methods: {
    normalizeProfile(profile) {
      const distanceMeters = profile.distanceMeters || profile.DistanceMeters
      const displayName = profile.displayName || profile.DisplayName || [profile.firstName, profile.lastName].filter(Boolean).join(' ') || 'Finder Profile'
      const distance = typeof distanceMeters === 'number'
        ? distanceMeters < 1000
          ? `${Math.max(distanceMeters, 1)} m`
          : `${(distanceMeters / 1000).toFixed(1)} km`
        : 'Nearby'

      return {
        id: profile.id || profile.CharacterUuid,
        characterUuid: profile.characterUuid || profile.CharacterUuid || null,
        firstName: profile.firstName || '',
        lastName: profile.lastName || '',
        displayName,
        gender: profile.gender || profile.Gender || null,
        age: profile.age || profile.Age || null,
        avatarUrl: profile.avatarUrl || profile.AvatarUrl || null,
        gradient: profile.gradient || fallbackGradient,
        level: profile.level || profile.Level || null,
        phoneNumber: profile.phoneNumber || profile.PhoneNumber || null,
        online: Boolean(profile.online || profile.Online),
        distanceMeters: distanceMeters || null,
        distanceLabel: distance,
        headline: profile.headline || profile.Headline || 'Los Santos local',
        bio: profile.bio || profile.Bio || 'No bio yet.',
        tags: Array.isArray(profile.tags) ? profile.tags : Array.isArray(profile.Tags) ? profile.Tags : []
      }
    },

    profileImageStyle(profile) {
      if (profile && profile.avatarUrl) {
        return { backgroundImage: `url(${profile.avatarUrl})` }
      }

      return { background: (profile && profile.gradient) || fallbackGradient }
    },

    getPoint(event) {
      const source = event.touches && event.touches.length ? event.touches[0] : event.changedTouches && event.changedTouches.length ? event.changedTouches[0] : event
      return { x: source.clientX, y: source.clientY }
    },

    startDrag(event) {
      if (!this.currentCard || this.leavingDirection) return
      const point = this.getPoint(event)
      this.drag.active = true
      this.drag.startX = point.x
      this.drag.startY = point.y
      this.drag.x = 0
      this.drag.y = 0

      window.addEventListener('mousemove', this.onDragMove)
      window.addEventListener('mouseup', this.endDrag)
      window.addEventListener('touchmove', this.onDragMove, { passive: false })
      window.addEventListener('touchend', this.endDrag)
      window.addEventListener('touchcancel', this.endDrag)
    },

    onDragMove(event) {
      if (!this.drag.active) return
      if (event.cancelable) event.preventDefault()
      const point = this.getPoint(event)
      this.drag.x = point.x - this.drag.startX
      this.drag.y = point.y - this.drag.startY
    },

    endDrag() {
      if (!this.drag.active) return
      const action = Math.abs(this.drag.x) >= SWIPE_THRESHOLD ? (this.drag.x > 0 ? 'like' : 'pass') : null
      this.drag.active = false
      this.removeDragListeners()

      if (action) {
        this.completeSwipe(action)
      } else {
        this.resetDrag()
      }
    },

    completeSwipe(action) {
      if (!this.currentCard || this.leavingDirection) return
      const direction = action === 'like' ? 1 : -1
      this.leavingDirection = action
      this.drag.x = direction * SWIPE_EXIT
      this.drag.y = -28

      // TODO: send Finder like/pass to backend after database phase is approved.
      window.setTimeout(() => {
        this.activeIndex += 1
        this.leavingDirection = null
        this.resetDrag()
      }, 260)
    },

    resetDrag() {
      this.drag.x = 0
      this.drag.y = 0
    },

    removeDragListeners() {
      window.removeEventListener('mousemove', this.onDragMove)
      window.removeEventListener('mouseup', this.endDrag)
      window.removeEventListener('touchmove', this.onDragMove)
      window.removeEventListener('touchend', this.endDrag)
      window.removeEventListener('touchcancel', this.endDrag)
    },

    openChat(match) {
      this.activeMatch = match
      this.activeScreen = 'chat'
    },

    requestFinderData() {
      if (window.mp && window.mp.trigger) {
        window.mp.trigger('phone::finder::loadProfile')
        window.mp.trigger('phone::finder::loadProfiles')
      }
    },

    saveProfile() {
      const payload = {
        DisplayName: this.profile.name,
        Age: Number(this.profile.age) || null,
        Gender: this.existingCharacterData.gender,
        Bio: this.profile.bio,
        Headline: 'Los Santos local',
        AvatarUrl: null,
        Tags: this.profile.tags,
        LookingForGender: null,
        MinAge: 18,
        MaxAge: 99,
        IsActive: true
      }

      if (window.mp && window.mp.trigger) {
        window.mp.trigger('phone::finder::saveProfile', JSON.stringify(payload))
        window.mp.trigger('phone::finder::loadProfiles')
      }

      this.activeScreen = 'discover'
    }
  },

  watch: {
    'finderState.profile': {
      handler(profile) {
        if (!this.finderState.isProfileLoaded) return

        if (!profile) {
          this.activeScreen = 'setup'
          return
        }

        this.profile.name = profile.DisplayName || profile.displayName || this.profile.name
        this.profile.age = String(profile.Age || profile.age || this.profile.age)
        this.profile.bio = profile.Bio || profile.bio || this.profile.bio
        this.profile.tags = profile.Tags || profile.tags || this.profile.tags
      },
      immediate: true
    }
  },

  mounted() {
    this.$store.commit('smartphone/setColorTheme', { header: 'dark', button: 'light' })
    this.requestFinderData()
  },

  beforeDestroy() {
    this.removeDragListeners()
  }
}
</script>

<style lang="scss" scoped>
.finder-tab {
  width: 100%;
  height: 100%;
  position: relative;
  overflow: hidden;
  padding: 2rem .85rem 3.25rem;
  color: #fff;
  background:
    radial-gradient(circle at 18% 9%, rgba(255, 76, 112, .38), transparent 32%),
    radial-gradient(circle at 92% 2%, rgba(255, 126, 63, .28), transparent 30%),
    linear-gradient(160deg, #130b13 0%, #21101b 46%, #09080d 100%);
  font-family: 'Akrobat', sans-serif;

  &__orb {
    position: absolute;
    border-radius: 50%;
    filter: blur(1.1rem);
    opacity: .55;
    pointer-events: none;

    &--one {
      width: 6rem;
      height: 6rem;
      top: 4rem;
      right: -2.4rem;
      background: #ff743d;
    }

    &--two {
      width: 7rem;
      height: 7rem;
      bottom: 7rem;
      left: -3rem;
      background: #ff0f78;
    }
  }

  &__top {
    position: relative;
    z-index: 1;
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: .9rem;
  }

  &__settings {
    width: 2rem;
    height: 2rem;
    border-radius: 50%;
    background:
      radial-gradient(circle at 50% 32%, rgba(255, 255, 255, .82) 0 .11rem, transparent .12rem),
      radial-gradient(circle at 50% 50%, rgba(255, 255, 255, .82) 0 .11rem, transparent .12rem),
      radial-gradient(circle at 50% 68%, rgba(255, 255, 255, .82) 0 .11rem, transparent .12rem),
      linear-gradient(135deg, rgba(255, 255, 255, .16), rgba(255, 255, 255, .04));
    border: 1px solid rgba(255, 255, 255, .1);
    backdrop-filter: blur(.55rem);
  }
}

.finder-brand {
  display: flex;
  align-items: center;
  gap: .5rem;

  img {
    width: 2.3rem;
    height: 2.3rem;
    border-radius: .7rem;
    box-shadow: 0 .35rem 1.1rem rgba(255, 39, 106, .36);
  }

  &__name {
    font-size: 1.05rem;
    line-height: .95rem;
    font-weight: 900;
    letter-spacing: .03rem;
  }

  &__caption {
    margin-top: .18rem;
    font-size: .54rem;
    color: rgba(255, 255, 255, .55);
    text-transform: uppercase;
    letter-spacing: .08rem;
  }
}

.finder-screen {
  position: relative;
  z-index: 1;
  height: 25.3rem;
  border-radius: 1.25rem;
}

.finder-welcome,
.finder-setup,
.finder-matches,
.finder-chat {
  padding: 1rem;
  background: linear-gradient(145deg, rgba(255, 255, 255, .105), rgba(255, 255, 255, .035));
  border: 1px solid rgba(255, 255, 255, .105);
  box-shadow:
    0 1rem 2rem rgba(0, 0, 0, .38),
    inset 0 1px 0 rgba(255, 255, 255, .12);
  backdrop-filter: blur(.85rem);
}

.finder-welcome {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;

  &__logo {
    width: 5.2rem;
    height: 5.2rem;
    border-radius: 1.45rem;
    margin-bottom: 1.3rem;
    box-shadow: 0 1rem 2.4rem rgba(255, 31, 105, .34);

    img {
      width: 100%;
      height: 100%;
    }
  }

  h2 {
    font-size: 1.85rem;
    line-height: 1.7rem;
    font-weight: 900;
    margin: .35rem 0 .65rem;
  }

  p {
    color: rgba(255, 255, 255, .72);
    font-size: .75rem;
    line-height: 1rem;
    margin-bottom: 1rem;
  }
}

.finder-kicker,
.finder-section-title small {
  color: #ff9b74;
  text-transform: uppercase;
  font-size: .56rem;
  letter-spacing: .1rem;
  font-weight: 800;
}

.finder-primary,
.finder-ghost {
  width: 100%;
  height: 2.45rem;
  border-radius: 999px;
  font-size: .78rem;
  font-weight: 900;
  letter-spacing: .04rem;
}

.finder-primary {
  color: #fff;
  background: linear-gradient(100deg, #ff136f, #ff743d);
  box-shadow: 0 .6rem 1.35rem rgba(255, 43, 100, .32);
}

.finder-ghost {
  margin-top: .55rem;
  color: rgba(255, 255, 255, .82);
  background: rgba(255, 255, 255, .08);
}

.finder-section-title {
  margin-bottom: .85rem;

  &--inline {
    display: flex;
    align-items: flex-start;
    justify-content: space-between;

    em {
      color: rgba(255, 255, 255, .48);
      font-style: normal;
      font-size: .62rem;
      font-weight: 900;
      padding-top: .25rem;
    }
  }

  span {
    display: block;
    font-size: 1.25rem;
    font-weight: 900;
  }

  small {
    display: block;
    margin-top: .18rem;
    color: rgba(255, 255, 255, .45);
  }
}

.finder-setup {
  label {
    display: block;
    margin-bottom: .62rem;
    color: rgba(255, 255, 255, .6);
    font-size: .62rem;
    letter-spacing: .05rem;
    text-transform: uppercase;
  }

  input,
  textarea {
    width: 100%;
    margin-top: .32rem;
    border-radius: .75rem;
    border: 1px solid rgba(255, 255, 255, .1);
    background: rgba(0, 0, 0, .26);
    color: #fff;
    padding: .62rem .7rem;
    font-size: .75rem;
    outline: none;
  }

  textarea {
    height: 4.6rem;
    resize: none;
  }

  &__chips {
    display: flex;
    flex-wrap: wrap;
    gap: .35rem;
    margin: .35rem 0 .85rem;

    span {
      padding: .35rem .55rem;
      border-radius: 999px;
      color: #ffd7e4;
      background: rgba(255, 34, 105, .12);
      font-size: .62rem;
    }
  }
}

.finder-profile-source {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: .5rem;
  margin: -.1rem 0 .55rem;
  padding: .52rem .65rem;
  border-radius: .72rem;
  background: rgba(255, 255, 255, .055);
  border: 1px solid rgba(255, 255, 255, .06);

  span {
    color: rgba(255, 255, 255, .45);
    font-size: .58rem;
    text-transform: uppercase;
    letter-spacing: .08rem;
  }

  strong {
    color: rgba(255, 255, 255, .78);
    font-size: .62rem;
    text-align: right;
  }
}

.finder-discover {
  background: transparent;
}

.finder-card-stack {
  position: relative;
  height: 21.1rem;
  perspective: 900px;
}

.finder-card {
  height: 21.1rem;
  overflow: hidden;
  border-radius: 1.25rem;
  background: rgba(12, 9, 14, .72);
  border: 1px solid rgba(255, 255, 255, .1);
  box-shadow: 0 1.15rem 2rem rgba(0, 0, 0, .42);
  user-select: none;
  touch-action: none;

  &--active,
  &--next {
    position: absolute;
    inset: 0;
  }

  &--active {
    z-index: 2;
    cursor: grab;

    &.is-dragging {
      cursor: grabbing;
    }
  }

  &--next {
    z-index: 1;
    opacity: .72;
    transform: scale(.965) translateY(.65rem);
    pointer-events: none;
    filter: saturate(.8) brightness(.78);
  }

  &__photo {
    height: 12.8rem;
    background-size: cover;
    background-position: center;
    position: relative;

    &:before {
      content: '';
      position: absolute;
      inset: 0;
      background:
        radial-gradient(circle at 50% 28%, rgba(255, 255, 255, .2), transparent 28%),
        linear-gradient(180deg, rgba(255, 255, 255, .06), rgba(0, 0, 0, .25));
    }
  }

  &__badge {
    position: absolute;
    left: .75rem;
    bottom: .75rem;
    padding: .34rem .58rem;
    border-radius: 999px;
    background: rgba(0, 0, 0, .48);
    color: #73ffba;
    font-size: .58rem;
    font-weight: 900;
    letter-spacing: .05rem;
    backdrop-filter: blur(.45rem);
  }

  &__stamp {
    position: absolute;
    z-index: 4;
    top: 1.15rem;
    padding: .34rem .62rem;
    border-radius: .45rem;
    font-size: 1rem;
    font-weight: 900;
    letter-spacing: .11rem;
    pointer-events: none;
    transform: rotate(-10deg);
    transition: opacity .1s linear;

    &--like {
      left: .9rem;
      color: #65ffb1;
      border: 2px solid rgba(101, 255, 177, .85);
      box-shadow: 0 0 1rem rgba(101, 255, 177, .28);
    }

    &--pass {
      right: .9rem;
      color: #ff647a;
      border: 2px solid rgba(255, 100, 122, .85);
      box-shadow: 0 0 1rem rgba(255, 100, 122, .28);
      transform: rotate(10deg);
    }
  }

  &__body {
    padding: .85rem;
  }

  &__name {
    font-size: 1.35rem;
    line-height: 1.1rem;
    font-weight: 900;

    span {
      color: rgba(255, 255, 255, .58);
      font-size: 1rem;
      margin-left: .25rem;
    }
  }

  &__meta {
    margin-top: .35rem;
    color: #ff9b74;
    font-size: .66rem;
    font-weight: 800;
  }

  p {
    margin-top: .48rem;
    color: rgba(255, 255, 255, .72);
    font-size: .72rem;
    line-height: .95rem;
  }

  &__tags {
    margin-top: .58rem;
    display: flex;
    flex-wrap: wrap;
    gap: .3rem;

    span {
      color: #fff;
      padding: .24rem .48rem;
      border-radius: 999px;
      background: rgba(255, 255, 255, .09);
      font-size: .58rem;
    }
  }
}

.finder-empty-card {
  height: 21.1rem;
  border-radius: 1.25rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  background: rgba(12, 9, 14, .58);
  border: 1px solid rgba(255, 255, 255, .08);

  strong {
    font-size: 1rem;
  }

  span {
    margin-top: .3rem;
    max-width: 10rem;
    color: rgba(255, 255, 255, .48);
    font-size: .66rem;
    line-height: .9rem;
  }
}

.finder-actions {
  display: flex;
  justify-content: center;
  gap: 1rem;
  margin-top: .7rem;
}

.finder-action {
  min-width: 3.05rem;
  height: 2.75rem;
  padding: 0 .85rem;
  border-radius: 999px;
  color: #fff;
  font-size: .68rem;
  text-transform: uppercase;
  letter-spacing: .08rem;
  font-weight: 900;
  backdrop-filter: blur(.7rem);

  &--pass {
    background: rgba(255, 255, 255, .12);
    border: 1px solid rgba(255, 255, 255, .14);
  }

  &--like {
    background: linear-gradient(135deg, #ff136f, #ff743d);
    box-shadow: 0 .55rem 1.2rem rgba(255, 31, 105, .35);
  }
}

.finder-match {
  width: 100%;
  display: flex;
  align-items: center;
  gap: .65rem;
  padding: .58rem;
  margin-bottom: .45rem;
  border-radius: .95rem;
  color: #fff;
  background: rgba(255, 255, 255, .075);

  &__avatar {
    width: 2.7rem;
    height: 2.7rem;
    border-radius: .78rem;
    background-size: cover;
    background-position: center;
    flex: 0 0 auto;
  }

  strong,
  span {
    display: block;
    text-align: left;
  }

  strong {
    font-size: .82rem;
  }

  span {
    margin-top: .16rem;
    color: rgba(255, 255, 255, .52);
    font-size: .64rem;
  }
}

.finder-chat {
  display: flex;
  flex-direction: column;

  &__head {
    display: flex;
    align-items: center;
    gap: .55rem;

    button {
      width: 1.9rem;
      height: 1.9rem;
      border-radius: 50%;
      background: rgba(255, 255, 255, .1);

      &:before {
        content: '<';
        color: #fff;
        font-size: 1.05rem;
      }
    }

    strong,
    span {
      display: block;
    }

    span {
      color: rgba(255, 255, 255, .5);
      font-size: .62rem;
    }
  }

  &__body {
    flex: 1;
    padding: 1rem 0;
  }

  &__todo {
    margin-top: .75rem;
    color: rgba(255, 255, 255, .42);
    font-size: .62rem;
    text-align: center;
  }

  &__input {
    display: flex;
    align-items: center;
    gap: .45rem;
    padding: .45rem;
    border-radius: 999px;
    background: rgba(0, 0, 0, .28);

    span {
      flex: 1;
      padding-left: .45rem;
      color: rgba(255, 255, 255, .4);
      font-size: .68rem;
    }

    button {
      color: #fff;
      border-radius: 999px;
      padding: .42rem .62rem;
      background: linear-gradient(100deg, #ff136f, #ff743d);
      font-size: .62rem;
      font-weight: 900;
    }
  }
}

.bubble {
  max-width: 82%;
  padding: .58rem .7rem;
  border-radius: .88rem;
  margin-bottom: .5rem;
  font-size: .72rem;
  line-height: .9rem;

  &--them {
    background: rgba(255, 255, 255, .1);
  }

  &--me {
    margin-left: auto;
    background: linear-gradient(100deg, #ff136f, #ff743d);
  }
}

.finder-nav {
  position: absolute;
  left: .9rem;
  right: .9rem;
  bottom: .78rem;
  z-index: 2;
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: .35rem;
  padding: .32rem;
  border-radius: 999px;
  background: rgba(0, 0, 0, .32);
  border: 1px solid rgba(255, 255, 255, .08);
  backdrop-filter: blur(.75rem);

  button {
    color: rgba(255, 255, 255, .52);
    height: 1.55rem;
    border-radius: 999px;
    font-size: .62rem;
    font-weight: 900;

    &.active {
      color: #fff;
      background: linear-gradient(100deg, rgba(255, 19, 111, .95), rgba(255, 116, 61, .95));
      box-shadow: 0 .35rem .8rem rgba(255, 31, 105, .22);
    }
  }
}

.finder-fade-enter-active,
.finder-fade-leave-active {
  transition: opacity .22s ease, transform .22s ease;
}

.finder-fade-enter,
.finder-fade-leave-to {
  opacity: 0;
  transform: translateY(.35rem) scale(.98);
}
</style>
