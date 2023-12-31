package com.goat.deathnote.global.oauth.dto;

import java.util.Collection;
import java.util.Collections;
import java.util.Map;

import com.goat.deathnote.domain.member.entity.Member;
import com.goat.deathnote.domain.member.entity.MemberRole;
import com.goat.deathnote.domain.member.entity.SocialProvider;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.oauth2.core.oidc.OidcIdToken;
import org.springframework.security.oauth2.core.oidc.OidcUserInfo;
import org.springframework.security.oauth2.core.oidc.user.OidcUser;
import org.springframework.security.oauth2.core.user.OAuth2User;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@RequiredArgsConstructor
public class MemberPrincipal implements OAuth2User, UserDetails, OidcUser {
	private final String email;
	private final String name;
	private final String nickname;
	private final String password;
	private final SocialProvider socialProvider;
	private final MemberRole roleType;
	private final Collection<GrantedAuthority> authorities;
	private Map<String, Object> attributes;

	public static MemberPrincipal create(Member member, MemberRole memberRole) {
		System.out.println(1);
		System.out.println(member);
		return new MemberPrincipal(member.getEmail(),
			member.getEmail(),
			member.getNickname(),
			member.getNickname(),
			member.getProvider(),
			memberRole,
			Collections.singletonList(new SimpleGrantedAuthority(memberRole.name())));
	}

	public static MemberPrincipal create(Member member, Map<String, Object> attributes, MemberRole memberRole) {
		MemberPrincipal memberPrincipal = create(member, memberRole);
		System.out.println(2);
		System.out.println(memberPrincipal);

		if (attributes != null) {
			memberPrincipal.setAttributes(attributes);
		}

		return memberPrincipal;
	}

	@Override
	public Map<String, Object> getAttributes() {
		return attributes;
	}

	@Override
	public Collection<GrantedAuthority> getAuthorities() {
		return authorities;
	}

	@Override
	public String getUsername() {
		return name;
	}

	@Override
	public boolean isAccountNonExpired() {
		return true;
	}

	@Override
	public boolean isAccountNonLocked() {
		return true;
	}

	@Override
	public boolean isCredentialsNonExpired() {
		return true;
	}

	@Override
	public boolean isEnabled() {
		return true;
	}

	@Override
	public Map<String, Object> getClaims() {
		return null;
	}

	@Override
	public OidcUserInfo getUserInfo() {
		return null;
	}

	@Override
	public OidcIdToken getIdToken() {
		return null;
	}

}